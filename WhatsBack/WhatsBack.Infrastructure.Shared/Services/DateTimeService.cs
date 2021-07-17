using WhatsBack.Application.Interfaces;
using System;

namespace WhatsBack.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
