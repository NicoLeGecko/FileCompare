﻿// <auto-generated />
using FileCompare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileCompare.Migrations
{
    [DbContext(typeof(FileCompareContext))]
    partial class FileCompareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("FileCompare.FileEntry", b =>
                {
                    b.Property<int>("FileEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullPath")
                        .HasColumnType("TEXT");

                    b.HasKey("FileEntryId");

                    b.ToTable("FileEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
