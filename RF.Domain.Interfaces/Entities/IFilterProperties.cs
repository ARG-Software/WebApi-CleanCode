namespace RF.Domain.Interfaces.Entities
{
    public interface IFilterProperties
    {
        int? SourceId { get; set; }

        int? LabelId { get; set; }

        int? SocietyId { get; set; }

        int? RoyaltyTypeId { get; set; }

        int? PublisherId { get; set; }

        int? ProductionTitleId { get; set; }

        int? PlatformTierId { get; set; }

        int? EpisodeId { get; set; }

        int? TerritoryId { get; set; }

        int? RoyaltyTypeGroupId { get; set; }

        int? PlatformTypeId { get; set; }

        int? ProductionTypeId { get; set; }

        int? RegionId { get; set; }
    }
}