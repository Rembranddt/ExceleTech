using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Infrastructure.BackgroundJobs.UpdateProductStatistics
{
    internal class UpdateProductStatisticsJobConfiguration : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var updateProductJobKey = JobKey.Create(nameof(UpdateProductStatisticsJob));

            options
             .AddJob<UpdateProductStatisticsJob>(JobBuilder => JobBuilder.WithIdentity(updateProductJobKey))
             .AddTrigger(trigger => trigger.ForJob(updateProductJobKey)
                                           .WithSimpleSchedule(schedule =>
                                            schedule.WithIntervalInSeconds(30).RepeatForever()));
        }
    }
}
