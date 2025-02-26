using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using NGEL.Data.Tables;
using NGEL.Data.Helpers;
using Lobby;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NGEL.Data.Helpers
{

    public static class AddDataTableServicesExtensions
    {
        public static void AddDataTableServices(this IServiceCollection Services)
        {
            Services.AddSingleton<DataTableService>();
            Services.AddSingleton<AccountLevelTableService>();
            Services.AddSingleton<AchievementDataTableService>();
            Services.AddSingleton<AchievementGroupDataTableService>();
            Services.AddSingleton<ArtifactDataTableService>();
            Services.AddSingleton<AssetDataTableService>();
            Services.AddSingleton<AttendanceDataTableService>();
            Services.AddSingleton<AttendanceRewardDataTableService>();
            Services.AddSingleton<AttributeDataTableService>();
            Services.AddSingleton<BattleStoreDataTableService>();
            Services.AddSingleton<ChapterDataTableService>();
            Services.AddSingleton<CharacterDataTableService>();
            Services.AddSingleton<CollectionDataTableService>();
            Services.AddSingleton<CollectionGroupDataTableService>();
            Services.AddSingleton<ColorDataTableService>();
            Services.AddSingleton<DataChipStoreListDataTableService>();
            Services.AddSingleton<EntitlementDataTableService>();
            Services.AddSingleton<EventStoreDataTableService>();
            Services.AddSingleton<ExpressionDataTableService>();
            Services.AddSingleton<GlobalDefineDataTableService>();
            Services.AddSingleton<GuideMissionDataTableService>();
            Services.AddSingleton<GuideMissionStepRewardTableService>();
            Services.AddSingleton<GlitchStoreDataTableService>();
            Services.AddSingleton<GoldMedalStoreDataTableService>();
            Services.AddSingleton<IncubationDataTableService>();
            Services.AddSingleton<InstantGuideUIDataTableService>();
            Services.AddSingleton<ItemTableService>();
            Services.AddSingleton<IntroduceDataTableService>();
            Services.AddSingleton<LevelUpBuffListDataTableService>();
            Services.AddSingleton<MissionDataTableService>();
            Services.AddSingleton<ParameterDataTableService>();
            Services.AddSingleton<PenaltyDataTableService>();
            Services.AddSingleton<PetDataTableService>();
            Services.AddSingleton<PetAbilityListDataTableService>();
            Services.AddSingleton<PetEggDataTableService>();
            Services.AddSingleton<PetEggGroupDataTableService>();
            Services.AddSingleton<ProfileDataTableService>();
            Services.AddSingleton<QuestDataTableService>();
            Services.AddSingleton<RewardDataTableService>();
            Services.AddSingleton<SeasonMissionCountDataTableService>();
            Services.AddSingleton<SeasonMissionListDataTableService>();
            Services.AddSingleton<SeasonPassDataTableService>();
            Services.AddSingleton<SeasonPassLevelDataTableService>();
            Services.AddSingleton<SeasonPassRewardDataTableService>();
            Services.AddSingleton<SilverMedalStoreDataTableService>();
            Services.AddSingleton<SkillDataTableService>();
            Services.AddSingleton<SkinDataTableService>();
            Services.AddSingleton<StringDataTableService>();
            Services.AddSingleton<UserBlockStringDataTableService>();
            Services.AddSingleton<TreasureBoxDataTableService>();
            Services.AddSingleton<VehicleDataTableService>();
            Services.AddSingleton<WeaponCategoryDataTableService>();
            Services.AddSingleton<WeaponCategoryUpgradeDataTableService>();
            Services.AddSingleton<WonderCubeDataTableService>();
            Services.AddSingleton<WonderCubeRwardDataTableService>();
            Services.AddSingleton<WonderStoreDataTableService>();
            Services.AddSingleton<WorldMapDataTableService>();
            Services.AddSingleton<BiskitLogEventIDTableService>();
            Services.AddSingleton<BlockContentDataTableService>();
            Services.AddSingleton<ErrorsDataTableService>();
            Services.AddSingleton<NavMenuDataTableService>();
        }
    }
}
