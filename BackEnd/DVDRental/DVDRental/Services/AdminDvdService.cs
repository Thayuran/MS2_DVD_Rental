using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;
using DVDRental.Repositories;

namespace DVDRental.Services
{
    public class AdminDvdService:IAdminDvdService
    {

        private readonly IAdminDvdRepository _dvdRepository;
        private readonly IAdminCategoriesRepository _categoriesRepository;

        public AdminDvdService(IAdminDvdRepository dvdRepository,IAdminCategoriesRepository categoriesRepository)
        {
            _dvdRepository = dvdRepository;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<DVDResponseDTO>> GetAllDVDsAsync()
        {
            var dvds = await _dvdRepository.GetAllDVDs();
            var dvdResponseList = new List<DVDResponseDTO>();

            foreach (var dvd in dvds)
            {
                dvdResponseList.Add(new DVDResponseDTO
                {
                    ID = dvd.ID,
                    MovieName = dvd.MovieName,
                    Categories = dvd.Categories.Select(c => c.CategoryID).ToList(),
                    ReleaseDate = dvd.ReleaseDate,
                    Director = dvd.Director,
                    Copies = dvd.Copies,
                    ImagePath = dvd.ImagePath
                });
            }
            return dvdResponseList;
        }

        public async Task<DVDResponseDTO> GetDVDByIdAsync(string id)
        {
            var dvd = await _dvdRepository.GetMovieById(id);

            if (dvd == null)
                throw new KeyNotFoundException($"DVD with ID {id} not found.");

            return new DVDResponseDTO
            {
                ID = dvd.ID,
                MovieName = dvd.MovieName,
                Categories = dvd.Categories.Select(c => c.CategoryID).ToList(),
                ReleaseDate = dvd.ReleaseDate,
                Director = dvd.Director,
                Copies = dvd.Copies,
                ImagePath = dvd.ImagePath
            };
        }

       
        public async Task<DVDResponseDTO> AddDVDAsync(DVDRequestDTO dvd)
        {
            if (string.IsNullOrEmpty(dvd.Title))
                throw new ArgumentException("Movie name cannot be empty.");

            var movieDvd = new MovieDvd
            {
                MovieName = dvd.Title,
                Categories = new List<Categories>(),
                ReleaseDate = dvd.ReleaseDate,
                Director = dvd.Director,
                Copies = dvd.Copies,
                ImagePath = dvd.Image.ToString()
            };

            foreach (var categoryId in dvd.CategoryIds)
            {
                var category = await _categoriesRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    movieDvd.Categories.Add(category);
                }
            }

            var result = await _dvdRepository.AddDVD(movieDvd);

            return new DVDResponseDTO
            {
                ID = result.ID,
                MovieName = result.MovieName,
                Categories = result.Categories.Select(c => c.CategoryID).ToList(),
                ReleaseDate = result.ReleaseDate,
                Director = result.Director,
                Copies = result.Copies,
                ImagePath = result.ImagePath
            };
        }

        public async Task UpdateDVDAsync(string id,DVDRequestDTO dvd)
        {
            var existingDVD = await _dvdRepository.GetMovieById(id);

            if (existingDVD == null)
                throw new KeyNotFoundException($"DVD with ID {id} not found.");

            existingDVD.MovieName = dvd.Title;
            existingDVD.ReleaseDate = dvd.ReleaseDate;
            existingDVD.Director = dvd.Director;
            existingDVD.Copies = dvd.Copies;
            existingDVD.ImagePath = dvd.Image.ToString();

            existingDVD.Categories.Clear();

            foreach (var categoryId in dvd.CategoryIds)
            {
                var category = await _categoriesRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    existingDVD.Categories.Add(category);
                }
            }
            await _dvdRepository.UpdateAsync(existingDVD);
        }

        public async Task DeleteDVDAsync(string id)
        {
            var existingDVD = await _dvdRepository.GetMovieById(id);

            if (existingDVD == null)
                throw new KeyNotFoundException($"DVD with ID {id} not found.");

            await _dvdRepository.DeleteAsync(id);
        }
    }
}
