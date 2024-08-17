using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Anemic
{
    public class Nazm_tspagentConfiguration : IEntityTypeConfiguration<Nazm_tspagent>
    {
        public void Configure(EntityTypeBuilder<Nazm_tspagent> builder)
        {
            builder.Property(I => I.indatim).HasColumnType("datetime");
            builder.Property(D => D.InqueryDate).HasColumnType("datetime");
            builder.Property(D => D.Update_Time).HasColumnType("datetime");
            builder.Property(D => D.Create_Time).HasColumnType("datetime");
            builder.Property(D => D.indati2m).HasColumnType("datetime");
            builder.Property(D => D.input_time).HasColumnType("datetime");
            builder.Property(A => A.Address).HasMaxLength(200);
            builder.Property(D => D.acn).HasMaxLength(14);
            builder.Property(D => D.bbc).HasMaxLength(4);
            builder.Property(D => D.bid).HasMaxLength(12);
            builder.Property(D => D.bpc).HasMaxLength(10);
            builder.Property(D => D.bpn).HasMaxLength(9);
            builder.Property(D => D.bsrn).HasMaxLength(12);
            builder.Property(D => D.consfee).HasMaxLength(18);
            builder.Property(D => D.cop).HasMaxLength(18);
            builder.Property(D => D.crn).HasMaxLength(12);
            builder.Property(D => D.cut).HasMaxLength(3);
            builder.Property(D => D.iinn).HasMaxLength(9);
            builder.Property(D => D.irtaxid).HasMaxLength(22);
            builder.Property(D => D.mu).HasMaxLength(18);
            builder.Property(D => D.odam).HasMaxLength(18);
            builder.Property(D => D.odr).HasMaxLength(255);
            builder.Property(D => D.odt).HasMaxLength(255);
            builder.Property(D => D.olt).HasMaxLength(255);
            builder.Property(D => D.pcn).HasMaxLength(16);
            builder.Property(D => D.pdt).HasMaxLength(13);
            builder.Property(D => D.pid).HasMaxLength(12);
            builder.Property(D => D.pmt).HasMaxLength(2);
            builder.Property(D => D.sbc).HasMaxLength(2);
            builder.Property(D => D.scc).HasMaxLength(5);
            builder.Property(D => D.scln).HasMaxLength(14);
            builder.Property(D => D.sstid).HasMaxLength(13);
            builder.Property(D => D.sstt).HasMaxLength(400);
            builder.Property(D => D.tins).HasMaxLength(14);
            builder.Property(D => D.TaxId).HasMaxLength(22);
            builder.Property(D => D.Reference_Id).HasMaxLength(36);
            builder.Property(D => D.Status).HasMaxLength(10);
            builder.Property(D => D.trmn).HasMaxLength(8);
            builder.Property(D => D.trn).HasMaxLength(14);
            builder.Property(D => D.tinb).HasMaxLength(14);
            builder.Property(D => D.Error_Description).HasMaxLength(512);
            builder.Property(D => D.Agent_Id).HasMaxLength(10);
            builder.Property(D => D.Mapfylddtlcod).HasMaxLength(10);
            builder.Property(D => D.Policy_No).HasMaxLength(100);
        }
    }
}
