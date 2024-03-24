namespace MotoDeliveryManager.Infrastructure.Database.PostgresDb.Configurations.EntityConfigurations;

public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlan>
{
    public void Configure(EntityTypeBuilder<RentalPlan> builder)
    {
        builder.ToTable("rental_plan");

        builder
            .HasKey(key => key.RentalPlanId)
            .HasName("rental_plan_pk");

        builder
            .Property(prop => prop.RentalPlanId)
            .HasColumnName("rental_plan_id");

        builder
            .Property(prop => prop.DurationDays)
            .HasColumnName("duration_days")
            .IsRequired();

        builder
            .Property(prop => prop.DailyCost)
            .HasColumnName("daily_cost")
            .IsRequired();

        builder
            .Property(prop => prop.PenaltyPercentage)
            .HasColumnName("penalty_percentage")
            .IsRequired();

        builder
            .Property(prop => prop.AdditionalDailyCost)
            .HasColumnName("additional_daily_cost")
            .IsRequired();

        builder
            .Property<DateTime>("created_at")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .Property<DateTime>("updated_at")
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasData(
                new
                {
                    RentalPlanId = new Guid("fc4ac394-4f6f-4405-9a3e-64aa8ca6f0d2"),
                    DurationDays = 7,
                    DailyCost = 30m,
                    PenaltyPercentage = 0.2m,
                    AdditionalDailyCost = 50m
                },
                new
                {
                    RentalPlanId = new Guid("6b354ecb-d9c9-4c6b-847f-ca92d06125d5"),
                    DurationDays = 15,
                    DailyCost = 28m,
                    PenaltyPercentage = 0.4m,
                    AdditionalDailyCost = 50m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new
                {
                    RentalPlanId = new Guid("b07cb1de-3c4d-43fb-9e68-0caaa42dda41"),
                    DurationDays = 30,
                    DailyCost = 22m,
                    PenaltyPercentage = 0.6m,
                    AdditionalDailyCost = 50m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
    }
}
