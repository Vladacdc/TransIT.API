using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Repositories;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.BLL.Tests.Services
{
    public abstract class CrudServiceTest<TEntity> where TEntity : class, IEntity, new()
    {
        protected Mock<IUnitOfWork> _unitOfWork;
        protected Mock<IBaseRepository<TEntity>> _repository;
        protected Mock<ILogger<ICrudService<TEntity>>> _logger;
        protected ICrudService<TEntity> _crudService;
        protected List<TEntity> _context;
        protected int _idCounter;
        public CrudServiceTest()
        {
            InitializeMocks();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetAsync_GivenValidId_ReturnsEntity(int id)
        {
            var result = await _crudService.GetAsync(id);

            Assert.Equal(result, _context[id - 1]);
        }

        [Fact]
        public async Task GetAsync_GivenInvalidId_ReturnsNull()
        {
            int id = 0;

            var result = await _crudService.GetAsync(id);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(5, 3)]
        [InlineData(0, 0)]
        public async Task GetAllAsync_GivenOffsetAndSize_ReturnsAllEntitiesInRange(uint offset, uint size)
        {
            var result = await _crudService.GetRangeAsync(offset, size);

            Assert.Equal(result, _context.Skip((int)offset).Take((int)size));
        }

        [Theory]
        [MemberData(nameof(SampleData))]
        public async Task CreateAsync_GivenEntity_AddsEntityToContext(TEntity entity)
        {
            await _crudService.CreateAsync(entity);

            Assert.Contains(entity, _context);
            Assert.Equal(entity.Id, _idCounter - 1);
            _repository.Verify(r => r.AddAsync(entity), Times.Once);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_GivenWrongEntity_ThrowsException()
        {
            var entity = new TEntity { Id = 1 };
            var exception = new DbUpdateException("", null as Exception);
            _repository.Setup(r => r.AddAsync(entity)).Throws(exception);
            
            await Assert.ThrowsAsync<DbUpdateException>(async()=> await _crudService.CreateAsync(entity));            
            _repository.Verify(r => r.AddAsync(entity), Times.Once);
            VerifyLogger();
        }

        [Fact]
        public async Task CreateAsync_GivenNull_ThrowsException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _crudService.CreateAsync(null));
            VerifyLogger();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task UpdateAsync_GivenExistingEntity_ReturnsUpdatedEntity(int id)
        {
            var entity = new TEntity { Id = id };
            int previousCount = _context.Count;

            var result = await _crudService.UpdateAsync(entity);

            Assert.Equal(result, entity);
            Assert.True(_context.Count == previousCount);
            _repository.Verify(r => r.Update(entity), Times.Once);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_GivenNull_ThrowsException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _crudService.UpdateAsync(null));
            VerifyLogger();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task DeleteAsync_GivenExistingEntityId_DeletesEntity(int id)
        {
            int previousCount = _context.Count;

            await _crudService.DeleteAsync(id);

            var entity = _context.Find(e => e.Id == id);
            Assert.Null(entity);
            Assert.True(_context.Count == previousCount - 1);
            _repository.Verify(r => r.Remove(It.IsAny<TEntity>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteAsync_GivenNonExistingEntityId_ReturnsNothing(int id)
        {
            int previousCount = _context.Count;

            await Assert.ThrowsAsync<NullReferenceException>(async () => await _crudService.DeleteAsync(id));   

            Assert.True(_context.Count == previousCount);
            _repository.Verify(r => r.Remove(It.IsAny<TEntity>()), Times.AtMostOnce);
            VerifyLogger();
        }

        [Fact]
        public async Task DeleteAsync_GivenDpUpdateExceptionInRepository_ReturnsNull()
        {
            var exception = new DbUpdateException("", null as Exception);
            _repository.Setup(r => r.Remove(It.IsAny<TEntity>())).Throws(exception);

            await _crudService.DeleteAsync(3);

            _repository.Verify(r => r.Remove(It.IsAny<TEntity>()), Times.Once);
            VerifyLogger();
        }

        [Fact]
        public async Task DeleteAsync_GivenExceptionInRepository_ThrowsException()
        {
            _repository.Setup(r => r.Remove(It.IsAny<TEntity>())).Throws(new NullReferenceException());
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _crudService.DeleteAsync(0));
            VerifyLogger();
        }

        protected void InitializeMocks()
        {
            InitializeContext();
            InitializeRepositoryMock();
            InitializeUnitOfWork();
            InitializeLogger();
            InitializeService();
        }

        protected void InitializeContext()
        {
            _idCounter = 5;
            _context = new List<TEntity>
            {
                new TEntity { Id = 1 },
                new TEntity { Id = 2 },
                new TEntity { Id = 3 },
                new TEntity { Id = 4 },
            };
        }

        protected void InitializeRepositoryMock()
        {
            _repository = new Mock<IBaseRepository<TEntity>>();

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(_context);

            _repository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _context.Find(g => g.Id == id));

            _repository.Setup(r => r.GetRangeAsync(It.IsAny<uint>(), It.IsAny<uint>()))
                .Returns<uint, uint>((offset, size) => Task.FromResult(_context.Skip((int)offset).Take((int)size)));

            _repository.Setup(r => r.AddAsync(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity =>
                {
                    entity.Id = _idCounter++;
                    _context.Add(entity);
                })
                .ReturnsAsync((TEntity entity) => entity);

            _repository.Setup(r => r.Remove(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity =>
                {
                    var founded = _context.Find(g => g.Id == entity.Id);
                    if (founded == null)
                    {
                        throw new DbUpdateException("", new Exception());
                    }
                    _context.Remove(founded);
                });

            _repository.Setup(r => r.Update(It.IsAny<TEntity>()))
                .Callback<TEntity>(entity => _context[_context.FindIndex(e => e.Id == entity.Id)] = entity);
        }

        protected void InitializeUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.SaveAsync()).ReturnsAsync(_context.Count);
        }

        protected void InitializeLogger()
        {
            _logger = new Mock<ILogger<ICrudService<TEntity>>>();
        }

        protected abstract void InitializeService();

        protected void VerifyLogger()
        {
            _logger.Verify(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
        }

        public static IEnumerable<object[]> SampleData =>
            new[]
            {
                new object [] { new TEntity { Id = 2 } },
                new object [] { new TEntity { Id = 0 } },
                new object [] { new TEntity { Id = 5 } }
            };
    }
}
