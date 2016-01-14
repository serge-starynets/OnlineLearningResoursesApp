using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using OnlineLearningResoursesApp.Models;

namespace OnlineLearningResoursesApp.Migrations
{
    [DbContext(typeof(LearningContext))]
    [Migration("20151228194701_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineLearningResoursesApp.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Duration");

                    b.Property<bool>("IsFree");

                    b.Property<int?>("LearningPlanId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("OnlineLearningResoursesApp.Models.LearningPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Duration");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.Property<string>("Speciality");

                    b.Property<bool>("Subscription");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("OnlineLearningResoursesApp.Models.Course", b =>
                {
                    b.HasOne("OnlineLearningResoursesApp.Models.LearningPlan")
                        .WithMany()
                        .HasForeignKey("LearningPlanId");
                });
        }
    }
}
