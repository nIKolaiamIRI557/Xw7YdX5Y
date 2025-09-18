// 代码生成时间: 2025-09-18 14:07:33
using System;
using System.Threading.Tasks;

namespace RandomNumberGenerator
{
    /// <summary>
    /// A service for generating random numbers using the Entity Framework framework.
    /// </summary>
    public class RandomNumberGeneratorService
    {
        private readonly IRandomNumberRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomNumberGeneratorService"/> class.
        /// </summary>
        /// <param name="repository">The repository that will handle the random number storage.</param>
        public RandomNumberGeneratorService(IRandomNumberRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Generates a random number and optionally stores it in the database.
        /// </summary>
        /// <param name="storeInDatabase">Indicates whether the generated number should be stored.</param>
        /// <returns>A task that represents the asynchronous operation and contains the generated random number.</returns>
        public async Task<int> GenerateRandomNumberAsync(bool storeInDatabase = false)
        {
            try
            {
                Random random = new Random();
                int randomNumber = random.Next();

                if (storeInDatabase)
                {
                    // Store the random number in the database
                    await _repository.AddRandomNumberAsync(randomNumber);
                }

                return randomNumber;
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

    /// <summary>
    /// Interface for the random number repository.
    /// </summary>
    public interface IRandomNumberRepository
    {
        /// <summary>
        /// Asynchronously adds a random number to the repository.
        /// </summary>
        /// <param name="number">The random number to be added.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddRandomNumberAsync(int number);
    }
}
