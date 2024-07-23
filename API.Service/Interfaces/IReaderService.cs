using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Service.Interfaces
{
    public interface IReaderService
    {
        public Task<string> AddToCurrentlyReadingAsync(int userId, int bookId);
        public Task<string> AddToReadAsync(int userId, int bookId);
        public Task<string> AddToWantToReadAsync(int userId, int bookId);
        public Task<string> RemoveFromReadAsync(int userId, int bookId);
        public Task<string> RemoveFromCurrentlyReadingAsync(int userId, int bookId);
        public Task<string> RemoveFromWantToReadListAsync(int userId, int bookId);

        public Task<User> GetUserWithCurrentlyReadingList(int userId);
        public Task<User> GetUserWithWantToReadList(int userId);
        public Task<User> GetUserWithReadList(int userId);
    }
}
