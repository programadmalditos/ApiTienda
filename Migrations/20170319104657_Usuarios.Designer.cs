using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ApiTienda.Model;

namespace ApiTienda.Migrations
{
    [DbContext(typeof(ListaCompraConext))]
    [Migration("20170319104657_Usuarios")]
    partial class Usuarios
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiTienda.Model.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.Property<decimal>("Precio");

                    b.Property<int?>("TiendaId");

                    b.Property<int>("Unidades");

                    b.HasKey("Id");

                    b.HasIndex("TiendaId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("ApiTienda.Model.Tienda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Tienda");
                });

            modelBuilder.Entity("ApiTienda.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ApiTienda.Model.Producto", b =>
                {
                    b.HasOne("ApiTienda.Model.Tienda", "Tienda")
                        .WithMany("Compras")
                        .HasForeignKey("TiendaId");
                });
        }
    }
}
