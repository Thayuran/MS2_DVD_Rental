using DVDRental.DTOs.RequestDTO;
using DVDRental.DTOs.ResponseDTO;
using DVDRental.Entities;
using DVDRental.Repositories;
using Microsoft.Data.SqlClient;

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

        //login
       /* public bool Login(AdminCredentials adminCredentials)
        {
            return _adminRepository.ValidateAdmin(adminCredentials);
        }
*/

        public async Task<List<DVDResponseDTO>> GetAllDVDsAsync()
        {
            var dvds = await _dvdRepository.GetAllDVDs();
            var dvdResponseList = new List<DVDResponseDTO>();

            foreach (var dvd in dvds)
            {
                dvdResponseList.Add(new DVDResponseDTO
                {
                    ID = dvd.ID,
                    MovieName = dvd.Title,
                    CategoryID = dvd.categoryid,
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
                MovieName = dvd.Title,
                CategoryID = dvd.categoryid,
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
                Title = dvd.Title,
                categoryid = dvd.CategoryId,
                ReleaseDate = dvd.ReleaseDate,
                Director = dvd.Director,
                Copies = dvd.Copies,
                ImagePath = dvd.Image.ToString()
            };

            /*foreach (var categoryId in dvd.CategoryIds)
            {
                var category = await _categoriesRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    movieDvd.Categories.Add(category);
                }
            }*/

            var result = await _dvdRepository.AddDVD(movieDvd);

            return new DVDResponseDTO
            {
                ID = result.ID,
                MovieName = result.Title,
                CategoryID = result.categoryid,
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

            existingDVD.Title = dvd.Title;
            existingDVD.ReleaseDate = dvd.ReleaseDate;
            existingDVD.Director = dvd.Director;
            existingDVD.Copies = dvd.Copies;
            existingDVD.ImagePath = dvd.Image.ToString();

            existingDVD.categoryid=dvd.CategoryId;

           /* foreach (var categoryId in dvd.CategoryIds)
            {
                var category = await _categoriesRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    existingDVD.Categories.Add(category);
                }
            }*/
            await _dvdRepository.UpdateAsync(existingDVD);
        }

        public async Task DeleteDVDAsync(string id)
        {
            var existingDVD = await _dvdRepository.GetMovieById(id);

            if (existingDVD == null)
                throw new KeyNotFoundException($"DVD with ID {id} not found.");

            await _dvdRepository.DeleteAsync(id);
        }

        //categories

       /* public async Task<List<DVDResponseDTO>> GetDVDsByCategoryAsync(int categoryId)
        {
            var category = await _categoriesRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found", nameof(categoryId));
            }

            var dvds = await _dvdRepository.GetDVDsByCategoryAsync(categoryId);
            return dvds.Select(MapToDVDResponseDTO).ToList();
        }*/

       /* public async Task AddDVDToCategoryAsync(string dvdId, int categoryId)
        {
            var dvd = await _dvdRepository.GetMovieById(dvdId);
            if (dvd == null)
            {
                throw new ArgumentException("DVD not found", nameof(dvdId));
            }

            var category = await _categoriesRepository.GetByIdAsync(categoryId);

            if (dvd.Categories == null)
            {
                dvd.Categories = new List<Categories>();
            }

            if (!dvd.Categories.Any(c => c.CategoryID == categoryId))
            {
                dvd.Categories.Add(category);
                await _dvdRepository.UpdateAsync(dvd);
            }
        }
*/
            /*public async Task RemoveDVDFromCategoryAsync(string dvdId, int categoryId)
        {
            var dvd = await _dvdRepository.GetMovieById(dvdId);
            if (dvd == null)
            {
                throw new ArgumentException("DVD not found", nameof(dvdId));
            }

           *//* if (dvd.Categories != categoryId)
            {
                throw new InvalidOperationException("DVD is not in the specified category");
            }

            dvd.CategoryId = null;
            await _dvdRepository.UpdateAsync(dvd);*//*

            
            var categoryToRemove = dvd.Categories?.FirstOrDefault(c => c.CategoryID == categoryId);
            if (categoryToRemove == null)
            {
                throw new InvalidOperationException("DVD is not in the specified category");
            }

            dvd.Categories.Remove(categoryToRemove);
            await _dvdRepository.UpdateAsync(dvd);
        }*/

          /*  private DVDResponseDTO MapToDVDResponseDTO(MovieDvd dvd)
        {
            return new DVDResponseDTO
            {
                ID = dvd.ID,
                MovieName = dvd.Title,
                Director= dvd.Director,
                ReleaseDate = dvd.ReleaseDate,
                ImagePath = dvd.ImagePath,
                Categories=dvd.Categories?.Select(c => c.CategoryID).ToList() ?? new List<int>(),
                Copies=dvd.Copies,
                
                
            };
        }
*/




    }
}
