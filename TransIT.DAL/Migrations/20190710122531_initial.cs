using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransIT.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISSUE_LOG", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSITION", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    FULL_NAME = table.Column<string>(maxLength: 500, nullable: false),
                    COUNTRY = table.Column<int>(nullable: true),
                    CURRENCY = table.Column<int>(nullable: true),
                    EDRPOU = table.Column<string>(maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIER", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BILL", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    NUMBER = table.Column<int>(nullable: true),
                    PRIORITY = table.Column<int>(nullable: false),
                    DATE = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISSUE", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    NEW_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    PATH = table.Column<string>(maxLength: 500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCUMENT_ISSUE_LOG",
                        column: x => x.ISSUE_LOG_ID,
                        principalTable: "ISSUE_LOG",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    WARRANTY_END_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    COMMISSIONING_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    LOCATION_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION_SUBGROUP", x => x.ID);
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP",
                        column: x => x.MALFUNCTION_SUBGROUP_ID,
                        principalTable: "MALFUNCTION_SUBGROUP",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    BOARD_NUMBER = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FIRST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    MIDDLE_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(maxLength: 50, nullable: true),
                    PHONE_NUMBER = table.Column<string>(maxLength: 15, nullable: true),
                    LOGIN = table.Column<string>(maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(maxLength: 100, nullable: false),
                    ROLE_ID = table.Column<int>(nullable: false),
                    IS_ACTIVE = table.Column<bool>(nullable: true, defaultValueSql: "((1))"),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_USER_ROLE",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_USER_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACTION_TYPE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_ACTION_TYPE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ACTION_TYPE_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COUNTRY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_COUNTRY_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_COUNTRY_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CURRENCY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SHORT_NAME = table.Column<string>(unicode: false, maxLength: 5, nullable: false),
                    FULL_NAME = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CURRENCY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_CURRENCY_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_CURRENCY_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_LOCATION_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_LOCATION_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MALFUNCTION_GROUP", x => x.ID);
                    table.ForeignKey(
                        name: "FK__MALFUNCTI__CREAT__73BA3083",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MALFUNCTI__MOD_I__74AE54BC",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POST", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_POST_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_POST_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    TRANS_NAME = table.Column<string>(maxLength: 50, nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_ROLE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_ROLE_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true),
                    IS_FIXED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STATE_USER_CREATE_ID",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STATE_USER_MOD_ID",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TOKEN",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    REFRESH_TOKEN = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    MOD_DATE = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOKEN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREATE_TOKEN_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_TOKEN_USER",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
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
                    CREATE_ID = table.Column<int>(nullable: true),
                    MOD_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE_TYPE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_TYPE_USER",
                        column: x => x.CREATE_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOD_VEHICLE_TYPE_ROLE",
                        column: x => x.MOD_ID,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "CREATE_ID", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 1, null, null, "ADMIN", "Адмін" });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "CREATE_ID", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 2, null, null, "WORKER", "Працівник" });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "CREATE_ID", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 3, null, null, "ENGINEER", "Інженер" });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "CREATE_ID", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 4, null, null, "REGISTER", "Реєстратор" });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "ID", "CREATE_ID", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 5, null, null, "ANALYST", "Аналітик" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 1, null, false, null, "NEW", "Нова" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 2, null, false, null, "VERIFIED", "Верифіковано" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 3, null, false, null, "REJECTED", "Відхилено" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 4, null, false, null, "TODO", "До виконання" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 5, null, false, null, "EXECUTING", "В роботі" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 6, null, false, null, "DONE", "Готово" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 7, null, false, null, "CONFIRMED", "Підтверджено" });

            migrationBuilder.InsertData(
                table: "STATE",
                columns: new[] { "ID", "CREATE_ID", "IS_FIXED", "MOD_ID", "NAME", "TRANS_NAME" },
                values: new object[] { 8, null, false, null, "UNCONFIRMED", "Не підтверджено" });

            migrationBuilder.CreateIndex(
                name: "IX_ACTION_TYPE_CREATE_ID",
                table: "ACTION_TYPE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ACTION_TYPE_MOD_ID",
                table: "ACTION_TYPE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__ACTION_T__D9C1FA00D8EDC403",
                table: "ACTION_TYPE",
                column: "NAME",
                unique: true);

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
                name: "IX_COUNTRY_MOD_ID",
                table: "COUNTRY",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__COUNTRY__D9C1FA008FF4E681",
                table: "COUNTRY",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CURRENCY_CREATE_ID",
                table: "CURRENCY",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CURRENCY_MOD_ID",
                table: "CURRENCY",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__CURRENCY__F4E7E33EEBE730B7",
                table: "CURRENCY",
                column: "SHORT_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_CREATE_ID",
                table: "DOCUMENT",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_ISSUE_LOG_ID",
                table: "DOCUMENT",
                column: "ISSUE_LOG_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENT_MOD_ID",
                table: "DOCUMENT",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_PATH_DOCUMENT",
                table: "DOCUMENT",
                column: "PATH",
                unique: true);

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
                name: "IX_EMPLOYEE_MOD_ID",
                table: "EMPLOYEE",
                column: "MOD_ID");

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
                name: "IX_ISSUE_MOD_ID",
                table: "ISSUE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ISSUE_STATE_ID",
                table: "ISSUE",
                column: "STATE_ID");

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
                name: "IX_ISSUE_LOG_MOD_ID",
                table: "ISSUE_LOG",
                column: "MOD_ID");

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
                name: "IX_POST_MOD_ID",
                table: "POST",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__POST__D9C1FA00297EABB2",
                table: "POST",
                column: "NAME",
                unique: true,
                filter: "[NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CREATE_ID",
                table: "ROLE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_MOD_ID",
                table: "ROLE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__ROLE__D9C1FA0001C36FF2",
                table: "ROLE",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_ROLE_TRANS_NAME",
                table: "ROLE",
                column: "TRANS_NAME",
                unique: true,
                filter: "[TRANS_NAME] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_STATE_CREATE_ID",
                table: "STATE",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STATE_MOD_ID",
                table: "STATE",
                column: "MOD_ID");

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
                name: "IX_SUPPLIER_MOD_ID",
                table: "SUPPLIER",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__SUPPLIER__D9C1FA0021944BFA",
                table: "SUPPLIER",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TOKEN_CREATE_ID",
                table: "TOKEN",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TOKEN_MOD_ID",
                table: "TOKEN",
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
                name: "IX_TRANSITION_MOD_ID",
                table: "TRANSITION",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSITION_TO_STATE_ID",
                table: "TRANSITION",
                column: "TO_STATE_ID");

            migrationBuilder.CreateIndex(
                name: "CK_ISSUE_TRANSITION_UNIQUE",
                table: "TRANSITION",
                columns: new[] { "FROM_STATE_ID", "ACTION_TYPE_ID", "TO_STATE_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_CREATE_ID",
                table: "USER",
                column: "CREATE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__USER__E39E2665C934E6A0",
                table: "USER",
                column: "LOGIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_MOD_ID",
                table: "USER",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLE_ID",
                table: "USER",
                column: "ROLE_ID");

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
                name: "IX_VEHICLE_TYPE_MOD_ID",
                table: "VEHICLE_TYPE",
                column: "MOD_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__VEHICLE___D9C1FA0095358636",
                table: "VEHICLE_TYPE",
                column: "NAME",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_ISSUE_LOG_USER",
                table: "ISSUE_LOG",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_ISSUE_LOG_USER",
                table: "ISSUE_LOG",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_LOG_ISSUE",
                table: "ISSUE_LOG",
                column: "ISSUE_ID",
                principalTable: "ISSUE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NEW_ISSUE_LOG_STATE",
                table: "ISSUE_LOG",
                column: "NEW_STATE_ID",
                principalTable: "STATE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OLD_ISSUE_LOG_STATE",
                table: "ISSUE_LOG",
                column: "OLD_STATE_ID",
                principalTable: "STATE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_LOG_ACTION_TYPE",
                table: "ISSUE_LOG",
                column: "ACTION_TYPE_ID",
                principalTable: "ACTION_TYPE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_LOG_SUPPLIER",
                table: "ISSUE_LOG",
                column: "SUPPLIER_ID",
                principalTable: "SUPPLIER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_ISSUE_TRANSITION_USER",
                table: "TRANSITION",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_ISSUE_TRANSITION_USER",
                table: "TRANSITION",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FROM_STATE",
                table: "TRANSITION",
                column: "FROM_STATE_ID",
                principalTable: "STATE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TO_STATE",
                table: "TRANSITION",
                column: "TO_STATE_ID",
                principalTable: "STATE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ACTION_TYPE_ISSUE",
                table: "TRANSITION",
                column: "ACTION_TYPE_ID",
                principalTable: "ACTION_TYPE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_SUPPLIER_USER",
                table: "SUPPLIER",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_SUPPLIER_USER",
                table: "SUPPLIER",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Country",
                table: "SUPPLIER",
                column: "COUNTRY",
                principalTable: "COUNTRY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currency",
                table: "SUPPLIER",
                column: "CURRENCY",
                principalTable: "CURRENCY",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_BILL_USER",
                table: "BILL",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_BILL_USER",
                table: "BILL",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BILL_DOCUMENT",
                table: "BILL",
                column: "DOCUMENT_ID",
                principalTable: "DOCUMENT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BILL_ISSUE",
                table: "BILL",
                column: "ISSUE_ID",
                principalTable: "ISSUE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_ISSUE_USER",
                table: "ISSUE",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_ISSUE_USER",
                table: "ISSUE",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_EMPLOYEE_ASSIGNED_TO",
                table: "ISSUE",
                column: "ASSIGNED_TO",
                principalTable: "EMPLOYEE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_MALFUNCTION",
                table: "ISSUE",
                column: "MALFUNCTION_ID",
                principalTable: "MALFUNCTION",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_STATE",
                table: "ISSUE",
                column: "STATE_ID",
                principalTable: "STATE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ISSUE_VEHICLE",
                table: "ISSUE",
                column: "VEHICLE_ID",
                principalTable: "VEHICLE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_DOCUMENT_USER",
                table: "DOCUMENT",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_DOCUMENT_USER",
                table: "DOCUMENT",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_VEHICLE_USER",
                table: "VEHICLE",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_VEHICLE_ROLE",
                table: "VEHICLE",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VEHICLE_LOCATION",
                table: "VEHICLE",
                column: "LOCATION_ID",
                principalTable: "LOCATION",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VEHICLE_VEHICLE_TYPE",
                table: "VEHICLE",
                column: "VEHICLE_TYPE_ID",
                principalTable: "VEHICLE_TYPE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_MALFUNCTION_SUBGROUP_USER",
                table: "MALFUNCTION_SUBGROUP",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_MALFUNCTION_SUBGROUP_USER",
                table: "MALFUNCTION_SUBGROUP",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP",
                table: "MALFUNCTION_SUBGROUP",
                column: "MALFUNCTION_GROUP_ID",
                principalTable: "MALFUNCTION_GROUP",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CREATE_MALFUNCTION_ROLE",
                table: "MALFUNCTION",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_MALFUNCTION_USER",
                table: "MALFUNCTION",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_EMPLOYEE_USER",
                table: "EMPLOYEE",
                column: "CREATE_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MOD_EMPLOYEE_ROLE",
                table: "EMPLOYEE",
                column: "MOD_ID",
                principalTable: "USER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EMPLOYEE_POST",
                table: "EMPLOYEE",
                column: "POST_ID",
                principalTable: "POST",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLE",
                table: "USER",
                column: "ROLE_ID",
                principalTable: "ROLE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CREATE_ROLE_USER",
                table: "ROLE");

            migrationBuilder.DropForeignKey(
                name: "FK_MOD_ROLE_USER",
                table: "ROLE");

            migrationBuilder.DropTable(
                name: "BILL");

            migrationBuilder.DropTable(
                name: "TOKEN");

            migrationBuilder.DropTable(
                name: "TRANSITION");

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
                name: "USER");

            migrationBuilder.DropTable(
                name: "ROLE");
        }
    }
}
