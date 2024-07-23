using API.Infrastructure.Data;
using API.Service.Interfaces;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Service.Implementations
{
    public class ReaderService : IReaderService
    {
        private readonly ApplicationDBContext _context;

        public ReaderService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> AddToCurrentlyReadingAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(u => u.ReadBooks)
                .Include(b => b.CurrentlyReading)
                .Include(b => b.WantToRead)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (!user.CurrentlyReading.Any(b => b.ProductId == bookId))
            {
                user.CurrentlyReading.Add(book);
                if (user.WantToRead.Contains(book))
                    user.WantToRead.Remove(book);
                if (user.ReadBooks.Contains(book))
                    user.ReadBooks.Remove(book);
                await _context.SaveChangesAsync();
            }

            return "Success";
        }


        public async Task<string> AddToReadAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(u => u.ReadBooks)
                .Include(b => b.CurrentlyReading)
                .Include(b => b.WantToRead)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (!user.ReadBooks.Any(b => b.ProductId == bookId))
            {
                user.ReadBooks.Add(book);
                if (user.CurrentlyReading.Contains(book))
                    user.CurrentlyReading.Remove(book);
                if (user.WantToRead.Contains(book))
                    user.WantToRead.Remove(book);
                await _context.SaveChangesAsync();
            }

            return "Success";
        }


        public async Task<string> AddToWantToReadAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(u => u.ReadBooks)
                .Include(b => b.CurrentlyReading)
                .Include(b => b.WantToRead)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (!user.WantToRead.Any(b => b.ProductId == bookId))
            {
                user.WantToRead.Add(book);
                if (user.CurrentlyReading.Contains(book))
                    user.CurrentlyReading.Remove(book);
                if (user.ReadBooks.Contains(book))
                    user.ReadBooks.Remove(book);
                await _context.SaveChangesAsync();
            }

            return "Success";
        }


        public async Task<string> RemoveFromCurrentlyReadingAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(b => b.CurrentlyReading)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (user.CurrentlyReading.Any(b => b.ProductId == bookId))
            {
                user.CurrentlyReading.Remove(book);
                await _context.SaveChangesAsync();
                return "Success";
            }
            return "BookNotInCurrentlyReading";

        }

        public async Task<string> RemoveFromReadAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(b => b.ReadBooks)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (user.ReadBooks.Any(b => b.ProductId == bookId))
            {
                user.ReadBooks.Remove(book);
                await _context.SaveChangesAsync();
                return "Success";
            }
            return "BookNotInCurrentlyReading";

        }

        public async Task<string> RemoveFromWantToReadListAsync(int userId, int bookId)
        {
            var user = await _context.Users
                .Include(b => b.WantToRead)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return "UserNotFound";

            var book = await _context.Products.FindAsync(bookId);
            if (book == null)
                return "BookNotFound";
            if (user.WantToRead.Any(b => b.ProductId == bookId))
            {
                user.WantToRead.Remove(book);
                await _context.SaveChangesAsync();
                return "Success";
            }
            return "BookNotInCurrentlyReading";

        }


        public async Task<User> GetUserWithCurrentlyReadingList(int userId)
        {
            return await _context.Users
            .Include(u => u.CurrentlyReading)
            .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
