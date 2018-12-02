namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CalendarApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CalendarApp.Models.CalendarAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CalendarApp.Models.CalendarAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Events.AddOrUpdate(e => e.Id,
                new Event() { Id = 1, EventName = "Game",
                    DateTime = new DateTime(2018, 12, 12, 18, 30, 00), Duration = 3 });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
