using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldDoomLeague.Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id_file = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    file_size = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    file_name = table.Column<string>(type: "varchar(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    upload_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_file);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    player_name = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    player_alias = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    fdbk_id_member = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roundflagtouchcaptures",
                columns: table => new
                {
                    id_roundflagtouchcapture = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    team = table.Column<string>(type: "enum('r','b')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    capture_number = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    gametic = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_roundflagtouchcapture);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Colour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
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
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
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
                name: "maps",
                columns: table => new
                {
                    id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_file = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    map_pack = table.Column<string>(type: "varchar(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    map_name = table.Column<string>(type: "varchar(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    map_number = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_map);
                    table.ForeignKey(
                        name: "fk_stats_Maps_Files",
                        column: x => x.fk_id_file,
                        principalTable: "files",
                        principalColumn: "id_file",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_wad_file = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    season_name = table.Column<string>(type: "varchar(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    date_start = table.Column<DateTime>(type: "date", nullable: false),
                    fk_id_team_winner = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_season);
                    table.ForeignKey(
                        name: "fk_stats_Seasons_Files",
                        column: x => x.fk_id_wad_file,
                        principalTable: "files",
                        principalColumn: "id_file",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsoverall",
                columns: table => new
                {
                    id_overall_stats = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    number_rounds_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    number_tics_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_environment_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage_with_flag = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_assists = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_flag_returns = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_killing_sprees = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_rampages = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_dominations = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_unstoppables = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_godlikes = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_wickedsicks = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_double_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_multi_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_ultra_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_monster_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_power_pickups = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_overall_stats);
                    table.ForeignKey(
                        name: "fk_stats_StatsOverall_Players",
                        column: x => x.fk_id_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ListId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Done = table.Column<bool>(nullable: false),
                    Reminder = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "statsoverallseason",
                columns: table => new
                {
                    id_overall_stats_season = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    number_tics_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    number_rounds_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_environment_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage_with_flag = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_assists = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_flag_returns = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_killing_sprees = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_rampages = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_dominations = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_unstoppables = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_godlikes = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_wickedsicks = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_double_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_multi_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_ultra_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_monster_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_power_pickups = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_overall_stats_season);
                    table.ForeignKey(
                        name: "fk_stats_StatsOverallSeason_Players",
                        column: x => x.fk_id_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_StatsOverallSeason_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id_team = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_captain = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_firstpick = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_secondpick = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_thirdpick = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    team_name = table.Column<string>(type: "varchar(64)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    team_abbreviation = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_team);
                    table.ForeignKey(
                        name: "fk_stats_Teams_Players_1",
                        column: x => x.fk_id_player_captain,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Teams_Players_2",
                        column: x => x.fk_id_player_firstpick,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Teams_Players_3",
                        column: x => x.fk_id_player_secondpick,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Teams_Players_4",
                        column: x => x.fk_id_player_thirdpick,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Teams_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "weeks",
                columns: table => new
                {
                    id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    week_number = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    week_type = table.Column<string>(type: "enum('n','p','f')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    week_start_date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_week);
                    table.ForeignKey(
                        name: "fk_stats_Weeks_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team_red = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team_blue = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    game_type = table.Column<string>(type: "enum('n','p','f')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    game_datetime = table.Column<DateTime>(nullable: true),
                    fk_id_team_winner = table.Column<uint>(type: "int(10) unsigned", nullable: true),
                    team_winner_color = table.Column<string>(type: "enum('r','b','t')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    fk_id_team_forfeit = table.Column<uint>(type: "int(10) unsigned", nullable: true),
                    team_forfeit_color = table.Column<string>(type: "enum('r','b')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_game);
                    table.ForeignKey(
                        name: "fk_stats_Games_Maps",
                        column: x => x.fk_id_map,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Games_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Games_Teams_blue",
                        column: x => x.fk_id_team_blue,
                        principalTable: "teams",
                        principalColumn: "id_team",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Games_Teams_red",
                        column: x => x.fk_id_team_red,
                        principalTable: "teams",
                        principalColumn: "id_team",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Games_Weeks",
                        column: x => x.fk_id_week,
                        principalTable: "weeks",
                        principalColumn: "id_week",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "demos",
                columns: table => new
                {
                    demo_id = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_game_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_player_id = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    is_uploaded = table.Column<byte>(type: "tinyint(1) unsigned", nullable: false),
                    player_lost_demo = table.Column<byte>(type: "tinyint(1) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.demo_id);
                    table.ForeignKey(
                        name: "fk_demo_game",
                        column: x => x.fk_game_id,
                        principalTable: "games",
                        principalColumn: "id_game",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_demo_player",
                        column: x => x.fk_player_id,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gameplayers",
                columns: table => new
                {
                    id_gameplayer = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    demo_not_taken = table.Column<string>(type: "enum('y','n')", nullable: false, defaultValueSql: "'n'")
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    demo_file_path = table.Column<string>(type: "varchar(128)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    MapsIdMap = table.Column<uint>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_gameplayer);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Games",
                        column: x => x.fk_id_game,
                        principalTable: "games",
                        principalColumn: "id_game",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Maps",
                        column: x => x.fk_id_map,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Players",
                        column: x => x.fk_id_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Teams",
                        column: x => x.fk_id_team,
                        principalTable: "teams",
                        principalColumn: "id_team",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GamePlayers_Weeks",
                        column: x => x.fk_id_week,
                        principalTable: "weeks",
                        principalColumn: "id_week",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gameplayers_maps_MapsIdMap",
                        column: x => x.MapsIdMap,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gameteamstats",
                columns: table => new
                {
                    id_gameteamstats = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    win = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    tie = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    loss = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    points = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    captures_for = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    captures_against = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    team_color = table.Column<string>(type: "enum('r','b')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    number_rounds_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    number_tics_played = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_environment_deaths = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_carrier_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_damage_with_flag = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_touches = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_assists = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_pickup_captures = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_flag_returns = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_killing_sprees = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_rampages = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_dominations = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_unstoppables = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_godlikes = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_spree_wickedsicks = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_double_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_multi_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_ultra_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_multi_monster_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    total_power_pickups = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_gameteamstats);
                    table.ForeignKey(
                        name: "fk_stats_GameTeamStats_Games",
                        column: x => x.fk_id_game,
                        principalTable: "games",
                        principalColumn: "id_game",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GameTeamStats_Maps",
                        column: x => x.fk_id_map,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GameTeamStats_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GameTeamStats_Teams",
                        column: x => x.fk_id_team,
                        principalTable: "teams",
                        principalColumn: "id_team",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_GameTeamStats_Weeks",
                        column: x => x.fk_id_week,
                        principalTable: "weeks",
                        principalColumn: "id_week",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    round_number = table.Column<uint>(type: "int(10) unsigned", nullable: true),
                    round_datetime = table.Column<DateTime>(nullable: true),
                    round_parse_version = table.Column<ushort>(type: "smallint(5) unsigned", nullable: true),
                    round_tics_duration = table.Column<uint>(type: "int(11) unsigned", nullable: true),
                    round_winner = table.Column<string>(type: "enum('r','b','t')", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_round);
                    table.ForeignKey(
                        name: "fk_stats_Rounds_Games",
                        column: x => x.fk_id_game,
                        principalTable: "games",
                        principalColumn: "id_game",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Rounds_Maps",
                        column: x => x.fk_id_map,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Rounds_Seasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_Rounds_Weeks",
                        column: x => x.fk_id_week,
                        principalTable: "weeks",
                        principalColumn: "id_week",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roundplayers",
                columns: table => new
                {
                    id_roundplayer = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    round_tics_duration = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_roundplayer);
                    table.ForeignKey(
                        name: "fk_stats_RoundPlayers_Players",
                        column: x => x.fk_id_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_RoundPlayers_Rounds",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsaccuracydata",
                columns: table => new
                {
                    id_stats_accuracy_data = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    hit_miss_ratio = table.Column<double>(type: "double unsigned", nullable: false),
                    sprite_percent = table.Column<double>(type: "double unsigned", nullable: false),
                    pinpoint_percent = table.Column<double>(type: "double unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_accuracy_data);
                    table.ForeignKey(
                        name: "fk_stats_StatsAccuracyData_PlayersAttacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_StatsAccuracyData_PlayersTarget",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_StatsAccuracyData_Rounds",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsaccuracyflagoutdata",
                columns: table => new
                {
                    id_stats_accuracy_flagout_data = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    hit_miss_ratio = table.Column<double>(type: "double unsigned", nullable: false),
                    sprite_percent = table.Column<double>(type: "double unsigned", nullable: false),
                    pinpoint_percent = table.Column<double>(type: "double unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_accuracy_flagout_data);
                    table.ForeignKey(
                        name: "fk_stataccuracyflagout_player_attacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stataccuracyflagout_player_target",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stataccuracyflagout_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsdamagecarrierdata",
                columns: table => new
                {
                    id_stats_carrier_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    damage_health = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    damage_green_armor = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    damage_blue_armor = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_carrier_damage);
                    table.ForeignKey(
                        name: "fk_statscarrierdamage_player_attacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statscarrierdamage_player_target",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statscarrierdamage_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsdamagedata",
                columns: table => new
                {
                    id_stats_damage = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    damage_health = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    damage_green_armor = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    damage_blue_armor = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_damage);
                    table.ForeignKey(
                        name: "fk_statsdamage_player_attacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statsdamage_player_target",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statsdamage_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statskillcarrierdata",
                columns: table => new
                {
                    id_stats_killcarrier = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    total_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_killcarrier);
                    table.ForeignKey(
                        name: "fk_statskillcarrierplayer_attacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statskillcarrier_player_target",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statskillcarrier_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statskilldata",
                columns: table => new
                {
                    id_stats_kill = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_attacker = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_player_target = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    weapon_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    total_kills = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_kill);
                    table.ForeignKey(
                        name: "fk_statskillplayer_attacker",
                        column: x => x.fk_id_player_attacker,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statskill_player_target",
                        column: x => x.fk_id_player_target,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statskill_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statspickupdata",
                columns: table => new
                {
                    id_stat_pickup = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_activator_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    pickup_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    pickup_amount = table.Column<uint>(type: "mediumint(8) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stat_pickup);
                    table.ForeignKey(
                        name: "fk_statpickup_player",
                        column: x => x.fk_id_activator_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_statpickup_round",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "statsrounds",
                columns: table => new
                {
                    id_stats_round = table.Column<uint>(type: "int(10) unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fk_id_player = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_round = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_game = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_week = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_map = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_season = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    fk_id_team = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    team = table.Column<string>(type: "enum('r','b')", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:Collation", "utf8mb4_unicode_ci"),
                    accuracy_complete_hits = table.Column<int>(type: "int(10)", nullable: false),
                    accuracy_complete_misses = table.Column<int>(type: "int(11)", nullable: false),
                    total_assists = table.Column<int>(type: "int(11)", nullable: false),
                    total_captures = table.Column<int>(type: "int(11)", nullable: false),
                    damage_output_between_touch_capture_max = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    damage_output_between_touch_capture_average = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    capture_tics_min = table.Column<int>(type: "int(11)", nullable: true),
                    capture_tics_max = table.Column<int>(type: "int(11)", nullable: true),
                    capture_tics_average = table.Column<double>(nullable: true),
                    capture_health_min = table.Column<int>(type: "int(11)", nullable: true),
                    capture_health_max = table.Column<int>(type: "int(11)", nullable: true),
                    capture_health_average = table.Column<double>(nullable: true),
                    capture_green_armor_min = table.Column<int>(type: "int(11)", nullable: true),
                    capture_green_armor_max = table.Column<int>(type: "int(11)", nullable: true),
                    capture_green_armor_average = table.Column<double>(nullable: true),
                    capture_blue_armor_min = table.Column<int>(type: "int(11)", nullable: true),
                    capture_blue_armor_max = table.Column<int>(type: "int(11)", nullable: true),
                    capture_blue_armor_average = table.Column<double>(nullable: true),
                    capture_with_super_pickups = table.Column<int>(type: "int(11)", nullable: false),
                    carriers_killed_while_holding_flag = table.Column<int>(type: "int(11)", nullable: false),
                    highest_kills_before_capturing = table.Column<int>(type: "int(11)", nullable: false),
                    total_pickup_captures = table.Column<int>(type: "int(11)", nullable: false),
                    pickup_capture_tics_min = table.Column<int>(type: "int(11)", nullable: true),
                    pickup_capture_tics_max = table.Column<int>(type: "int(11)", nullable: true),
                    pickup_capture_tics_average = table.Column<double>(nullable: true),
                    total_damage = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_green_armor = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_blue_armor = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_flag_carrier = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_taken_environment = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_carrier_taken_environment = table.Column<int>(type: "int(11)", nullable: false),
                    total_damage_with_flag = table.Column<int>(type: "int(11)", nullable: false),
                    total_flag_returns = table.Column<int>(type: "int(11)", nullable: false),
                    total_kills = table.Column<int>(type: "int(11)", nullable: false),
                    total_carrier_kills = table.Column<int>(type: "int(11)", nullable: false),
                    total_deaths = table.Column<int>(type: "int(11)", nullable: false),
                    total_environment_deaths = table.Column<int>(type: "int(11)", nullable: false),
                    total_environment_carrier_deaths = table.Column<int>(type: "int(11)", nullable: false),
                    amount_team_kills = table.Column<int>(type: "int(11)", nullable: false),
                    spree_killing_sprees = table.Column<int>(type: "int(11)", nullable: false),
                    spree_rampage = table.Column<int>(type: "int(11)", nullable: false),
                    spree_dominations = table.Column<int>(type: "int(11)", nullable: false),
                    spree_unstoppables = table.Column<int>(type: "int(11)", nullable: false),
                    spree_godlikes = table.Column<int>(type: "int(11)", nullable: false),
                    spree_wickedsicks = table.Column<int>(type: "int(11)", nullable: false),
                    multi_double_kills = table.Column<int>(type: "int(11)", nullable: false),
                    multi_multi_kills = table.Column<int>(type: "int(11)", nullable: false),
                    multi_ultra_kills = table.Column<int>(type: "int(11)", nullable: false),
                    multi_monster_kills = table.Column<int>(type: "int(11)", nullable: false),
                    longest_spree = table.Column<int>(type: "int(11)", nullable: false),
                    highest_multi_frags = table.Column<int>(type: "int(11)", nullable: false),
                    total_power_pickups = table.Column<int>(type: "int(11)", nullable: false),
                    pickup_health_gained = table.Column<int>(type: "int(11)", nullable: false),
                    health_from_nonpower_pickups = table.Column<int>(type: "int(11)", nullable: false),
                    total_touches = table.Column<int>(type: "int(11)", nullable: false),
                    total_pickup_touches = table.Column<int>(type: "int(11)", nullable: false),
                    touch_health_min = table.Column<int>(type: "int(11)", nullable: true),
                    touch_health_max = table.Column<int>(type: "int(11)", nullable: true),
                    touch_health_average = table.Column<double>(nullable: true),
                    touch_green_armor_min = table.Column<int>(type: "int(11)", nullable: true),
                    touch_green_armor_max = table.Column<int>(type: "int(11)", nullable: true),
                    touch_green_armor_average = table.Column<double>(nullable: true),
                    touch_blue_armor_min = table.Column<int>(type: "int(11)", nullable: true),
                    touch_blue_armor_max = table.Column<int>(type: "int(11)", nullable: true),
                    touch_blue_armor_average = table.Column<double>(nullable: true),
                    touch_health_result_capture_min = table.Column<int>(type: "int(11)", nullable: true),
                    touch_health_result_capture_max = table.Column<int>(type: "int(11)", nullable: true),
                    touch_health_result_capture_average = table.Column<double>(nullable: true),
                    touches_with_over_hundred_health = table.Column<int>(type: "int(11)", nullable: false),
                    efficiency_points = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_stats_round);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblGames",
                        column: x => x.fk_id_game,
                        principalTable: "games",
                        principalColumn: "id_game",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblMaps",
                        column: x => x.fk_id_map,
                        principalTable: "maps",
                        principalColumn: "id_map",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblPlayers",
                        column: x => x.fk_id_player,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblRounds",
                        column: x => x.fk_id_round,
                        principalTable: "rounds",
                        principalColumn: "id_round",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblSeasons",
                        column: x => x.fk_id_season,
                        principalTable: "seasons",
                        principalColumn: "id_season",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_stats_tblStatsRounds_tblWeeks",
                        column: x => x.fk_id_week,
                        principalTable: "weeks",
                        principalColumn: "id_week",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "demo_id_UNIQUE",
                table: "demos",
                column: "demo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_demo_game_idx",
                table: "demos",
                column: "fk_game_id");

            migrationBuilder.CreateIndex(
                name: "fk_demo_player_idx",
                table: "demos",
                column: "fk_player_id");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "id_file_UNIQUE",
                table: "files",
                column: "id_file",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Games_idx",
                table: "gameplayers",
                column: "fk_id_game");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Map_idx",
                table: "gameplayers",
                column: "fk_id_map");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Players_idx",
                table: "gameplayers",
                column: "fk_id_player");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Seasons_idx",
                table: "gameplayers",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Teams_idx",
                table: "gameplayers",
                column: "fk_id_team");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GamePlayers_Weeks_idx",
                table: "gameplayers",
                column: "fk_id_week");

            migrationBuilder.CreateIndex(
                name: "id_gameplayer_UNIQUE",
                table: "gameplayers",
                column: "id_gameplayer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gameplayers_MapsIdMap",
                table: "gameplayers",
                column: "MapsIdMap");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Games_Map_idx",
                table: "games",
                column: "fk_id_map");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Games_Seasons_idx",
                table: "games",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Games_Teams_blue_idx",
                table: "games",
                column: "fk_id_team_blue");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Games_Teams_red_idx",
                table: "games",
                column: "fk_id_team_red");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Games_Weeks_idx",
                table: "games",
                column: "fk_id_week");

            migrationBuilder.CreateIndex(
                name: "id_games_UNIQUE",
                table: "games",
                column: "id_game",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_GameTeamStats_Games_idx",
                table: "gameteamstats",
                column: "fk_id_game");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GameTeamStats_Maps_idx",
                table: "gameteamstats",
                column: "fk_id_map");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GameTeamStats_Seasons_idx",
                table: "gameteamstats",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GameTeamStats_Teams_idx",
                table: "gameteamstats",
                column: "fk_id_team");

            migrationBuilder.CreateIndex(
                name: "fk_stats_GameTeamStats_Weeks_idx",
                table: "gameteamstats",
                column: "fk_id_week");

            migrationBuilder.CreateIndex(
                name: "id_gameteamstats_UNIQUE",
                table: "gameteamstats",
                column: "id_gameteamstats",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_Maps_Files_idx",
                table: "maps",
                column: "fk_id_file");

            migrationBuilder.CreateIndex(
                name: "id_map_UNIQUE",
                table: "maps",
                column: "id_map",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "fdbk_id_member",
                table: "players",
                column: "fdbk_id_member",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_player_UNIQUE",
                table: "players",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "player_name_UNIQUE",
                table: "players",
                column: "player_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundFlagTouchCaptures_game_idx",
                table: "roundflagtouchcaptures",
                column: "fk_id_game");

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundFlagTouchCaptures_player_idx",
                table: "roundflagtouchcaptures",
                column: "fk_id_player");

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundFlagTouchCaptures_round_idx",
                table: "roundflagtouchcaptures",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundFlagTouchCaptures_team_idx",
                table: "roundflagtouchcaptures",
                column: "fk_id_team");

            migrationBuilder.CreateIndex(
                name: "id_roundflagtouchcapture_UNIQUE",
                table: "roundflagtouchcaptures",
                column: "id_roundflagtouchcapture",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundPlayers_Players_idx",
                table: "roundplayers",
                column: "fk_id_player");

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundPlayers_Rounds_idx",
                table: "roundplayers",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_roundplayer_UNIQUE",
                table: "roundplayers",
                column: "id_roundplayer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_RoundPlayers_Teams_idx",
                table: "roundplayers",
                column: "round_tics_duration");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Rounds_Games_idx",
                table: "rounds",
                column: "fk_id_game");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Rounds_Maps_idx",
                table: "rounds",
                column: "fk_id_map");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Rounds_Seasons_idx",
                table: "rounds",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Rounds_Weeks_idx",
                table: "rounds",
                column: "fk_id_week");

            migrationBuilder.CreateIndex(
                name: "id_round_UNIQUE",
                table: "rounds",
                column: "id_round",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_Seasons_WadFile_idx",
                table: "seasons",
                column: "fk_id_wad_file");

            migrationBuilder.CreateIndex(
                name: "id_season_UNIQUE",
                table: "seasons",
                column: "id_season",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_player_attacker_idx",
                table: "statsaccuracydata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_player_target_idx",
                table: "statsaccuracydata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_round_idx",
                table: "statsaccuracydata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_accuracy_data_UNIQUE",
                table: "statsaccuracydata",
                column: "id_stats_accuracy_data",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_player_attacker_idx",
                table: "statsaccuracyflagoutdata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_player_target_idx",
                table: "statsaccuracyflagoutdata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_stataccuracy_round_idx",
                table: "statsaccuracyflagoutdata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_accuracy_data_UNIQUE",
                table: "statsaccuracyflagoutdata",
                column: "id_stats_accuracy_flagout_data",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_attacker_idx",
                table: "statsdamagecarrierdata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_target_idx",
                table: "statsdamagecarrierdata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_round_idx",
                table: "statsdamagecarrierdata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_damage_UNIQUE",
                table: "statsdamagecarrierdata",
                column: "id_stats_carrier_damage",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_attacker_idx",
                table: "statsdamagedata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_target_idx",
                table: "statsdamagedata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_round_idx",
                table: "statsdamagedata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_damage_UNIQUE",
                table: "statsdamagedata",
                column: "id_stats_damage",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_attacker_idx",
                table: "statskillcarrierdata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_target_idx",
                table: "statskillcarrierdata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_round_idx",
                table: "statskillcarrierdata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_damage_UNIQUE",
                table: "statskillcarrierdata",
                column: "id_stats_killcarrier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_attacker_idx",
                table: "statskilldata",
                column: "fk_id_player_attacker");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_player_target_idx",
                table: "statskilldata",
                column: "fk_id_player_target");

            migrationBuilder.CreateIndex(
                name: "fk_statsdamage_round_idx",
                table: "statskilldata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stats_damage_UNIQUE",
                table: "statskilldata",
                column: "id_stats_kill",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_id_player",
                table: "statsoverall",
                column: "fk_id_player",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_seasonstats_players_idx",
                table: "statsoverallseason",
                column: "fk_id_player");

            migrationBuilder.CreateIndex(
                name: "fk_seasonstats_season_idx",
                table: "statsoverallseason",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_statpickup_player_idx",
                table: "statspickupdata",
                column: "fk_id_activator_player");

            migrationBuilder.CreateIndex(
                name: "fk_statpickup_round_idx",
                table: "statspickupdata",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "id_stat_pickup_UNIQUE",
                table: "statspickupdata",
                column: "id_stat_pickup",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_StatsRounds_Games_idx",
                table: "statsrounds",
                column: "fk_id_game");

            migrationBuilder.CreateIndex(
                name: "fk_stats_StatsRounds_Maps_idx",
                table: "statsrounds",
                column: "fk_id_map");

            migrationBuilder.CreateIndex(
                name: "fk_statround_player_idx",
                table: "statsrounds",
                column: "fk_id_player");

            migrationBuilder.CreateIndex(
                name: "fk_statround_round_idx",
                table: "statsrounds",
                column: "fk_id_round");

            migrationBuilder.CreateIndex(
                name: "fk_stats_StatsRounds_Seasons_idx",
                table: "statsrounds",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "fk_stats_StatsRounds_Teams_idx",
                table: "statsrounds",
                column: "fk_id_team");

            migrationBuilder.CreateIndex(
                name: "fk_stats_StatsRounds_Weeks_idx",
                table: "statsrounds",
                column: "fk_id_week");

            migrationBuilder.CreateIndex(
                name: "id_stats_round_UNIQUE",
                table: "statsrounds",
                column: "id_stats_round",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_stats_Teams_1_idx",
                table: "teams",
                column: "fk_id_player_captain");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Teams_Players_2_idx",
                table: "teams",
                column: "fk_id_player_firstpick");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Teams_Players_3_idx",
                table: "teams",
                column: "fk_id_player_secondpick");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Teams_Players_4_idx",
                table: "teams",
                column: "fk_id_player_thirdpick");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Teams_Season_idx",
                table: "teams",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "id_team_UNIQUE",
                table: "teams",
                column: "id_team",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "fk_stats_Weeks_Season_idx",
                table: "weeks",
                column: "fk_id_season");

            migrationBuilder.CreateIndex(
                name: "id_week_UNIQUE",
                table: "weeks",
                column: "id_week",
                unique: true);
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
                name: "demos");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "gameplayers");

            migrationBuilder.DropTable(
                name: "gameteamstats");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "roundflagtouchcaptures");

            migrationBuilder.DropTable(
                name: "roundplayers");

            migrationBuilder.DropTable(
                name: "statsaccuracydata");

            migrationBuilder.DropTable(
                name: "statsaccuracyflagoutdata");

            migrationBuilder.DropTable(
                name: "statsdamagecarrierdata");

            migrationBuilder.DropTable(
                name: "statsdamagedata");

            migrationBuilder.DropTable(
                name: "statskillcarrierdata");

            migrationBuilder.DropTable(
                name: "statskilldata");

            migrationBuilder.DropTable(
                name: "statsoverall");

            migrationBuilder.DropTable(
                name: "statsoverallseason");

            migrationBuilder.DropTable(
                name: "statspickupdata");

            migrationBuilder.DropTable(
                name: "statsrounds");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "maps");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "weeks");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "files");
        }
    }
}
