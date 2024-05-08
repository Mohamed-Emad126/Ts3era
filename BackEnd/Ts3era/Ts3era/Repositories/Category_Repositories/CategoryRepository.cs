using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Ts3era.Dto.Category_Dto;
using Ts3era.Dto.Category_Dto.Category_Dto;
using Ts3era.HandleResponseApi;
using Ts3era.Models;
using Ts3era.Models.Data;

namespace Ts3era.Repositories.Category_Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CategoryRepository> logger;
        private new List<string> allowextention = new List<string>() { ".jpg", ".png" };
        private long maxsizeimage = 109951163;//2MB


        public CategoryRepository
            (
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<CategoryRepository>logger
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

    

        public async Task<List<CategoryDetailsDto>> GetAll()
        {
            var category =await context.Categories.Include(c=>c.subCategories).ToListAsync();
            var map=mapper.Map<List<CategoryDetailsDto>>(category);
            return map;
        }

        public async Task<CategoryDetailsDto> GetById(int id)
        {
            var category = await context.Categories
                .Include(c => c.subCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
            
            
            var map =mapper.Map<CategoryDetailsDto>(category);
            return map;
        }

        public async Task<CategoryDetailsDto> GetByName(string name)
        {
            var category =await context.Categories
                .Include(c => c.subCategories)
                .FirstOrDefaultAsync(c=>c.Name == name);    
            var map = mapper.Map<CategoryDetailsDto>(category);    
            return map;
        }



        public async Task<CreateCategoryDto> Create(CreateCategoryDto dto)
        {
            if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName.ToLower())))
                throw new Exception("Must be Allawed .png or ,jpg");
            if (dto.Image.Length > maxsizeimage)
                throw new Exception("Max Allowed image is 2MB");

            using var datastream = new MemoryStream();
            await dto.Image.CopyToAsync(datastream);
            var category = mapper.Map<Category>(dto);
            category.Image=datastream.ToArray();
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return dto;
            
        }

        public async Task<string> Update(int id, UpdateCategoryDto dto)
        {

            var category =await context.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            if (category == null)
                throw new Exception($"No Category Was Found With ID{id}");

            if (dto.Image!=null )
            {

                if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName.ToLower())))
                    throw new Exception("Must be Allawed .png or .jpg");
                if (dto.Image.Length > maxsizeimage)
                    throw new Exception("Max Allowed image is 2MB");

                using var datastream = new MemoryStream();
                await dto.Image.CopyToAsync (datastream);
                category.Image = datastream.ToArray();
            }
            category.Name = dto.CategoryName;
            
            context.Categories.Update(category);
            context.SaveChanges();
            return "Updated ";




        }

        public List<CategoryDetailsDto> Search(string? name)
        {
            var category = context.Categories
                .Where(c=>c.Name.Trim().ToLower()
                .Contains(name.Trim().ToLower()));
            //if (!category.Any(c => c.Name == name))
            //    throw new Exception("Not Found Category");
             
            var map = mapper.Map<List<CategoryDetailsDto>>(category);
            return map;
                
        }

        public async Task  Delete(int id)
        {

            var old = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (old == null || id != old.Id)
            {
                throw new Exception($"Not Found Category With ID {id}");
            }
            else
            {
                context.Categories.Remove(old);
                context.SaveChanges();
            }
               
        }

        public Task<bool> isvaliidcategory(int id)
        {
           var isvalid= context.Categories.AnyAsync(c => c.Id == id);
            
            return isvalid;
        }

        public async Task<int> GetCountCategory()
        {
            var count =await context.Categories.CountAsync();
            return count;
        }



        #region oldDelete 
        //public async Task Delete(int id)
        //{
        //    try
        //    {
        //        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        //        if (category == null || id == 0)
        //            throw new ArgumentNullException("Not Found Category");

        //        context.Categories.Remove(category);
        //        context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {

        //        logger.LogError(ex.Message);
        //    }

    }

        #endregion

    
}
