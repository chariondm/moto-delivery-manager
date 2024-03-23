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
            .IsRequired();

        builder
            .Property<DateTime>("updated_at")
            .IsRequired();
    }
}
