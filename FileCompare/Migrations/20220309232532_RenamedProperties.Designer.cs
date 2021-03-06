// <auto-generated />
using System;
using FileCompare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileCompare.Migrations
{
    [DbContext(typeof(FileCompareContext))]
    [Migration("20220309232532_RenamedProperties")]
    partial class RenamedProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("FileCompare.FileEntry", b =>
                {
                    b.Property<int>("FileEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullPath")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("HashAsBytes")
                        .HasColumnType("BLOB");

                    b.HasKey("FileEntryId");

                    b.ToTable("FileEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
