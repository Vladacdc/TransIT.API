using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransIT.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FIRST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    MIDDLE_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    IS_ACTIVE = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CREATE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ACTION_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACTION_TYPE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_ACTION_TYPE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ACTION_TYPE_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    TRANS_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CREATE_ROLE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ROLE_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COUNTRY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_COUNTRY_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_COUNTRY_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CURRENCY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SHORT_NAME = table.Column<string>(maxLength: 5, nullable: false),
                    FULL_NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENCY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_CURRENCY_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_CURRENCY_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LOCATION",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_LOCATION_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_LOCATION_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MALFUNCTION_GROUP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION_GROUP", x => x.ID);
                    table.ForeignKey(
                        name: "FK__MALFUNCTI__CREAT__73BA3083",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MALFUNCTI__MOD_I__74AE54BC",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "POST",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_POST_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_POST_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STATE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: true),
                    TRANS_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false),
                    CreateId = table.Column<string>(nullable: true),
                    ModId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STATE_AspNetUsers_CreateId",
                        column: x => x.CreateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STATE_AspNetUsers_ModId",
                        column: x => x.ModId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VEHICLE_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE_TYPE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_TYPE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_TYPE_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIER",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    FULL_NAME = table.Column<string>(maxLength: 500, nullable: false),
                    COUNTRY = table.Column<int>(nullable: true),
                    CURRENCY = table.Column<int>(nullable: true),
                    EDRPOU = table.Column<string>(maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Country",
                        column: x => x.COUNTRY,
                        principalTable: "COUNTRY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CREATE_SUPPLIER_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Currency",
                        column: x => x.CURRENCY,
                        principalTable: "CURRENCY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_SUPPLIER_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MALFUNCTION_SUBGROUP",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: false),
                    MALFUNCTION_GROUP_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION_SUBGROUP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_MALFUNCTION_SUBGROUP_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP",
                        column: x => x.MALFUNCTION_GROUP_ID,
                        principalTable: "MALFUNCTION_GROUP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_MALFUNCTION_SUBGROUP_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FIRST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    MIDDLE_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    SHORT_NAME = table.Column<string>(maxLength: 50, nullable: false),
                    POST_ID = table.Column<int>(nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    BOARD_NUMBER = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_EMPLOYEE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_POST",
                        column: x => x.POST_ID,
                        principalTable: "POST",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_EMPLOYEE_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TRANSITION",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FROM_STATE_ID = table.Column<int>(nullable: false),
                    TO_STATE_ID = table.Column<int>(nullable: false),
                    ACTION_TYPE_ID = table.Column<int>(nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSITION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACTION_TYPE_ISSUE",
                        column: x => x.ACTION_TYPE_ID,
                        principalTable: "ACTION_TYPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CREATE_ISSUE_TRANSITION_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FROM_STATE",
                        column: x => x.FROM_STATE_ID,
                        principalTable: "STATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TO_STATE",
                        column: x => x.TO_STATE_ID,
                        principalTable: "STATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ISSUE_TRANSITION_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VEHICLE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VEHICLE_TYPE_ID = table.Column<int>(nullable: true),
                    VINCODE = table.Column<string>(maxLength: 20, nullable: false),
                    INVENTORY_ID = table.Column<string>(maxLength: 40, nullable: true),
                    REG_NUM = table.Column<string>(maxLength: 15, nullable: true),
                    BRAND = table.Column<string>(maxLength: 50, nullable: true),
                    MODEL = table.Column<string>(maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    WARRANTY_END_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    COMMISSIONING_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    LOCATION_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VEHICLE_LOCATION",
                        column: x => x.LOCATION_ID,
                        principalTable: "LOCATION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VEHICLE_VEHICLE_TYPE",
                        column: x => x.VEHICLE_TYPE_ID,
                        principalTable: "VEHICLE_TYPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MALFUNCTION",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: false),
                    MALFUNCTION_SUBGROUP_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_MALFUNCTION_ROLE",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP",
                        column: x => x.MALFUNCTION_SUBGROUP_ID,
                        principalTable: "MALFUNCTION_SUBGROUP",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_MALFUNCTION_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ISSUE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SUMMARY = table.Column<string>(nullable: true),
                    WARRANTY = table.Column<int>(nullable: true),
                    DEADLINE = table.Column<DateTime>(type: "datetime", nullable: true),
                    STATE_ID = table.Column<int>(nullable: true, defaultValueSql: "((1))"),
                    ASSIGNED_TO = table.Column<int>(nullable: true),
                    VEHICLE_ID = table.Column<int>(nullable: false),
                    MALFUNCTION_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    NUMBER = table.Column<int>(nullable: true),
                    PRIORITY = table.Column<int>(nullable: false),
                    DATE = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISSUE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ISSUE_EMPLOYEE_ASSIGNED_TO",
                        column: x => x.ASSIGNED_TO,
                        principalTable: "EMPLOYEE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CREATE_ISSUE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISSUE_MALFUNCTION",
                        column: x => x.MALFUNCTION_ID,
                        principalTable: "MALFUNCTION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISSUE_STATE",
                        column: x => x.STATE_ID,
                        principalTable: "STATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ISSUE_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISSUE_VEHICLE",
                        column: x => x.VEHICLE_ID,
                        principalTable: "VEHICLE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ISSUE_LOG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    EXPENSES = table.Column<decimal>(type: "decimal(10, 2)", nullable: true),
                    OLD_STATE_ID = table.Column<int>(nullable: true),
                    NEW_STATE_ID = table.Column<int>(nullable: true),
                    SUPPLIER_ID = table.Column<int>(nullable: true),
                    ACTION_TYPE_ID = table.Column<int>(nullable: true),
                    ISSUE_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISSUE_LOG", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ISSUE_LOG_ACTION_TYPE",
                        column: x => x.ACTION_TYPE_ID,
                        principalTable: "ACTION_TYPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CREATE_ISSUE_LOG_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISSUE_LOG_ISSUE",
                        column: x => x.ISSUE_ID,
                        principalTable: "ISSUE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NEW_ISSUE_LOG_STATE",
                        column: x => x.NEW_STATE_ID,
                        principalTable: "STATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OLD_ISSUE_LOG_STATE",
                        column: x => x.OLD_STATE_ID,
                        principalTable: "STATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ISSUE_LOG_SUPPLIER",
                        column: x => x.SUPPLIER_ID,
                        principalTable: "SUPPLIER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ISSUE_LOG_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    ISSUE_LOG_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true),
                    NEW_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    PATH = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_DOCUMENT_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DOCUMENT_ISSUE_LOG",
                        column: x => x.ISSUE_LOG_ID,
                        principalTable: "ISSUE_LOG",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_DOCUMENT_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BILL",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SUM = table.Column<decimal>(type: "decimal(20, 2)", nullable: true),
                    DOCUMENT_ID = table.Column<int>(nullable: true),
                    ISSUE_ID = table.Column<int>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<string>(nullable: true),
                    MOD_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BILL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_BILL_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BILL_DOCUMENT",
                        column: x => x.DOCUMENT_ID,
                        principalTable: "DOCUMENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BILL_ISSUE",
                        column: x => x.ISSUE_ID,
                        principalTable: "ISSUE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_BILL_USER",
                        column: x => x.MOD_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACTION_TYPE_CREATE_ID",
                table: "ACTION_TYPE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__ACTION_T__D9C1FA00D8EDC403",
                table: "ACTION_TYPE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ACTION_TYPE_MOD_ID",
                table: "ACTION_TYPE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CREATE_ID",
                table: "AspNetRoles",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_ROLE_TRANS_NAME",
                table: "AspNetRoles",
                column: "TRANS_NAME",
                unique: true,
                filter: "[TRANS_NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_MOD_ID",
                table: "AspNetRoles",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CREATE_ID",
                table: "AspNetUsers",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MOD_ID",
                table: "AspNetUsers",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BILL_CREATE_ID",
                table: "BILL",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BILL_DOCUMENT_ID",
                table: "BILL",
                column: "DOCUMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BILL_ISSUE_ID",
                table: "BILL",
                column: "ISSUE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BILL_MOD_ID",
                table: "BILL",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRY_CREATE_ID",
                table: "COUNTRY",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__COUNTRY__D9C1FA008FF4E681",
                table: "COUNTRY",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRY_MOD_ID",
                table: "COUNTRY",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CURRENCY_CREATE_ID",
                table: "CURRENCY",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__CURRENCY__F4E7E33EEBE730B7",
                table: "CURRENCY",
                column: "SHORT_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CURRENCY_MOD_ID",
                table: "CURRENCY",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_CREATE_ID",
                table: "DOCUMENT",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_ISSUE_LOG_ID",
                table: "DOCUMENT",
                column: "ISSUE_LOG_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_PATH_DOCUMENT",
                table: "DOCUMENT",
                column: "PATH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_MOD_ID",
                table: "DOCUMENT",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_EMPLOYEE_BOARD_NUMBER_UNIQUE",
                table: "EMPLOYEE",
                column: "BOARD_NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_CREATE_ID",
                table: "EMPLOYEE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_POST_ID",
                table: "EMPLOYEE",
                column: "POST_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_EMPLOYEE_SHORT_NAME_UNIQUE",
                table: "EMPLOYEE",
                column: "SHORT_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_MOD_ID",
                table: "EMPLOYEE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_ASSIGNED_TO",
                table: "ISSUE",
                column: "ASSIGNED_TO");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_CREATE_ID",
                table: "ISSUE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_MALFUNCTION_ID",
                table: "ISSUE",
                column: "MALFUNCTION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_STATE_ID",
                table: "ISSUE",
                column: "STATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_MOD_ID",
                table: "ISSUE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_VEHICLE_ID",
                table: "ISSUE",
                column: "VEHICLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_ACTION_TYPE_ID",
                table: "ISSUE_LOG",
                column: "ACTION_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_CREATE_ID",
                table: "ISSUE_LOG",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_ISSUE_ID",
                table: "ISSUE_LOG",
                column: "ISSUE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_NEW_STATE_ID",
                table: "ISSUE_LOG",
                column: "NEW_STATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_OLD_STATE_ID",
                table: "ISSUE_LOG",
                column: "OLD_STATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_SUPPLIER_ID",
                table: "ISSUE_LOG",
                column: "SUPPLIER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_LOG_MOD_ID",
                table: "ISSUE_LOG",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOCATION_CREATE_ID",
                table: "LOCATION",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOCATION_MOD_ID",
                table: "LOCATION",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_CREATE_ID",
                table: "MALFUNCTION",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_MALFUNCTION_SUBGROUP_ID",
                table: "MALFUNCTION",
                column: "MALFUNCTION_SUBGROUP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_MOD_ID",
                table: "MALFUNCTION",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_GROUP_CREATE_ID",
                table: "MALFUNCTION_GROUP",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_GROUP_MOD_ID",
                table: "MALFUNCTION_GROUP",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_SUBGROUP_CREATE_ID",
                table: "MALFUNCTION_SUBGROUP",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP_ID",
                table: "MALFUNCTION_SUBGROUP",
                column: "MALFUNCTION_GROUP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MALFUNCTION_SUBGROUP_MOD_ID",
                table: "MALFUNCTION_SUBGROUP",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_CREATE_ID",
                table: "POST",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__POST__D9C1FA00297EABB2",
                table: "POST",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_POST_MOD_ID",
                table: "POST",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STATE_CreateId",
                table: "STATE",
                column: "CreateId");

            migrationBuilder.CreateIndex(
                name: "IX_STATE_ModId",
                table: "STATE",
                column: "ModId");

            migrationBuilder.CreateIndex(
                name: "UQ_STATE_TRANS_NAME",
                table: "STATE",
                column: "TRANS_NAME",
                unique: true,
                filter: "[TRANS_NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SUPPLIER_COUNTRY",
                table: "SUPPLIER",
                column: "COUNTRY");

            migrationBuilder.CreateIndex(
                name: "IX_SUPPLIER_CREATE_ID",
                table: "SUPPLIER",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SUPPLIER_CURRENCY",
                table: "SUPPLIER",
                column: "CURRENCY");

            migrationBuilder.CreateIndex(
                name: "UQ__SUPPLIER__D9C1FA0021944BFA",
                table: "SUPPLIER",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SUPPLIER_MOD_ID",
                table: "SUPPLIER",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSITION_ACTION_TYPE_ID",
                table: "TRANSITION",
                column: "ACTION_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSITION_CREATE_ID",
                table: "TRANSITION",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSITION_TO_STATE_ID",
                table: "TRANSITION",
                column: "TO_STATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSITION_MOD_ID",
                table: "TRANSITION",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "CK_ISSUE_TRANSITION_UNIQUE",
                table: "TRANSITION",
                columns: new[] { "FROM_STATE_ID", "ACTION_TYPE_ID", "TO_STATE_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_CREATE_ID",
                table: "VEHICLE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_LOCATION_ID",
                table: "VEHICLE",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_MOD_ID",
                table: "VEHICLE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_VEHICLE_TYPE_ID",
                table: "VEHICLE",
                column: "VEHICLE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_VINCODE_UNIQUE",
                table: "VEHICLE",
                column: "VINCODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_TYPE_CREATE_ID",
                table: "VEHICLE_TYPE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__VEHICLE___D9C1FA0095358636",
                table: "VEHICLE_TYPE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_TYPE_MOD_ID",
                table: "VEHICLE_TYPE",
                column: "MOD_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BILL");

            migrationBuilder.DropTable(
                name: "TRANSITION");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DOCUMENT");

            migrationBuilder.DropTable(
                name: "ISSUE_LOG");

            migrationBuilder.DropTable(
                name: "ACTION_TYPE");

            migrationBuilder.DropTable(
                name: "ISSUE");

            migrationBuilder.DropTable(
                name: "SUPPLIER");

            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "MALFUNCTION");

            migrationBuilder.DropTable(
                name: "STATE");

            migrationBuilder.DropTable(
                name: "VEHICLE");

            migrationBuilder.DropTable(
                name: "COUNTRY");

            migrationBuilder.DropTable(
                name: "CURRENCY");

            migrationBuilder.DropTable(
                name: "POST");

            migrationBuilder.DropTable(
                name: "MALFUNCTION_SUBGROUP");

            migrationBuilder.DropTable(
                name: "LOCATION");

            migrationBuilder.DropTable(
                name: "VEHICLE_TYPE");

            migrationBuilder.DropTable(
                name: "MALFUNCTION_GROUP");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
