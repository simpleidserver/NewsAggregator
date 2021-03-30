﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.DataSources;

namespace NewsAggregator.EF.Configurations
{
    public class DatasourceConfiguration : IEntityTypeConfiguration<DataSourceAggregate>
    {
        public void Configure(EntityTypeBuilder<DataSourceAggregate> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasMany(r => r.ExtractionHistories).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(a => a.DomainEvts);
        }
    }
}