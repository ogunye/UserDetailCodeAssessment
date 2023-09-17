using CodeAssessment.Data;
using CodeAssessment.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;

namespace CodeAssessment.Repositories
{
    public class UserDetailRepository : IUserDetailsRepositroy
    {
        private readonly ApplicationDbContext _context;

        public UserDetailRepository(ApplicationDbContext context    )
        {
            _context = context;
        }
        public async Task<int> CreateUserAsync(UserDetail userDetail)
        {
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();
            return userDetail.Id;
        }

        public async Task<byte[]?> ExportAllToCsvAsync()
        {
            var userDetailList = await _context.UserDetails.ToListAsync();

            if ( userDetailList is null || userDetailList.Count == 0 )
            {
                return null; // No Data to export
            }

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

            await csvWriter.WriteRecordsAsync(userDetailList);
            await streamWriter.FlushAsync();

            return memoryStream.ToArray();
        }

        public async Task<IEnumerable<UserDetail>> GetAllUserAsync() => await _context.UserDetails.ToListAsync();
    }
}
