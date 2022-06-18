using System;
using System.Net;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
#nullable disable
namespace Infra.Data.Context
{
    public partial class VivoContext : DbContext
    {
        public static string DockerHostMachineIpAddress => Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();
        public VivoContext() { }

        public VivoContext(DbContextOptions<VivoContext> options)
            : base(options)
        {
        }
        

        public virtual DbSet<Cliente> Cliente { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DesafioVivo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasKey(e => e.ClienteID);
                entity.Property(e => e.ClienteID).HasColumnName("ID");

                entity.Property(e => e.NomeCompleto)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NOME_COMPLETO");

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasColumnName("DATA_NASCIMENTO");

                entity.Property(e => e.DocRgNumero)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("DOC_RG_NUMERO");

                entity.Property(e => e.DocRgOrgaoEmissor)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DOC_RG_ORGAO_EMISSOR");

                entity.Property(e => e.DocRgUF)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("DOC_RG_UF");

                entity.Property(e => e.DocCPF)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("DOC_CPF");

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ENDERECO");

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasColumnType("int")
                    .HasColumnName("ATIVO");

                entity.Property(e => e.DataCadastro)
                    .HasColumnType("datetime")
                    .IsRequired()
                    .HasColumnName("DATA_CADASTRO");

            });
        }
        }

}
