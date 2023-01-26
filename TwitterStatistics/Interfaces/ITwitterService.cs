using System.Threading.Tasks;

namespace TwitterStatistics
{
    public interface ITwitterService
    {
        public Task ReadDataAsync();
    }
}