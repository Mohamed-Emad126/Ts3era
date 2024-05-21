using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using Ts3era.Dto.SubCategory_Dto;
using Ts3era.Models;
using Ts3era.Models.Data;
using Ts3era.Repositories.Category_Repositories;

namespace Ts3era.Repositories.SubCategory_Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<SubCategoryRepository> logger;
        private readonly IWebHostEnvironment webHost;
        private new List<string> allowextention = new List<string>() { ".jpg", ".png" };
        private long maxsizeimage = 109951163;//2MB
        public SubCategoryRepository(
            ApplicationDbContext context,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            ILogger<SubCategoryRepository> logger,
            IWebHostEnvironment  webHost
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
            this.webHost = webHost;
        }



        public async Task<List<SubCategoryDetailsDto>> GetAll()
        {

            var subcategory = await context.SubCategories
                .Include(c => c.Category)
                .Include(p => p.Products)
                .ToListAsync();
            var map = mapper.Map<List<SubCategoryDetailsDto>>(subcategory);
            return map;
        }

        public async Task<SubCategoryDetailsDto> GetById(int id)
        {
            var subcategory = await context.SubCategories
                .Include(c => c.Category)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
            var map = mapper.Map<SubCategoryDetailsDto>(subcategory);
            return map;
        }

        public async Task<SubCategoryDetailsDto> Getname(string name)
        {
            var subcategory = await context.SubCategories
                .Include(c => c.Category)
                .Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Name == name);
            var map = mapper.Map<SubCategoryDetailsDto>(subcategory);
            return map;
        }

        public async Task<List<SubCategoryDetailsDto>> Search(string? name)
        {

            var search = await context.SubCategories
                .Where(c => c.Name.Trim().ToLower()
                .Contains(name.Trim().ToLower()))
                .ToListAsync();
            var map = mapper.Map<List<SubCategoryDetailsDto>>(search);
            return map;

        }
        public async Task<CreateSubCategoryDto> Create(CreateSubCategoryDto dto)
        {
            if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                throw new Exception(" Must Allawed .png OR .jpg ");
            if (dto.Image.Length > maxsizeimage)
                throw new Exception("Max  Allawed Image 2MB");

            var isvalid = await categoryRepository.isvaliidcategory(dto.Category_ID);
            if (!isvalid)
                throw new Exception("Ivalid Categgory ID ");



            var uploadfile = Path.Combine(webHost.WebRootPath, "Images/SubCategory");
            var uniquefile =Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
            var pathfile =Path.Combine(uploadfile, uniquefile);

            using var stream = new FileStream(pathfile, FileMode.Create);
            var subcategory = mapper.Map<SubCategory>(dto);
            dto.Image.CopyTo(stream);
            stream.Close();
            subcategory.Image= "Images/SubCategory/" +uniquefile.ToString();
            await context.SubCategories.AddAsync(subcategory);
            await context.SaveChangesAsync();
            return dto;



            /*using var datastream = new MemoryStream();
            await dto.Image.CopyToAsync(datastream);
            var subcategory = mapper.Map<SubCategory>(dto);
           // subcategory.Image = datastream.ToArray();
            await context.SubCategories.AddAsync(subcategory);
            await context.SaveChangesAsync();
            return dto;*/






        }

        public async Task<string> Update(int id, UpdateSubCategoryDto dto)
        {
            var subcategory =await context.SubCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (subcategory == null)
                throw new Exception($"No SubCategory Was Found With ID{id}");
            if (dto.Image != null)
            {
                if (!allowextention.Contains(Path.GetExtension(dto.Image.FileName).ToLower()))
                    throw new Exception(" Must Allawed .png OR .jpg");
                if (dto.Image.Length > maxsizeimage)
                    throw new Exception("Max  Allawed Image 2MB");


                var uploadfile = Path.Combine(webHost.WebRootPath, "Images/SubCategory");
                var uniquefile = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                var pathfile = Path.Combine(uploadfile, uniquefile);

                using var stream = new FileStream(pathfile, FileMode.Create);
                dto.Image.CopyTo(stream);
                stream.Close();
                subcategory.Image = "Images/SubCategory/" + uniquefile.ToString();
                /*  using var datastream = new MemoryStream();
                  await dto.Image.CopyToAsync(datastream);
                 // subcategory.Image= datastream.ToArray();*/

            }

            subcategory.Name = dto.SUbCategoryName;
            subcategory.Category_Id = dto.Category_ID;

            var isvalid = await categoryRepository.isvaliidcategory(dto.Category_ID);
            if (!isvalid)
                throw new Exception("Invalid Category ID");

            context.SubCategories.Update(subcategory);
            context.SaveChanges();
            return "Updated";

        }

        public async Task<List<CategoriesDto>> GetAllCategory()
        {
            var category =await context.Categories.OrderByDescending(c=>c.Name).ToListAsync();
            var map=mapper.Map<List<CategoriesDto>>(category);
            return map;
        }

        public async Task Delete(int id)
        {
            var old =await context.SubCategories.FirstOrDefaultAsync(c=> c.Id == id);
           
                if (old == null || id != old.Id)
                {
                    throw new Exception("Not Found SubCategory");
                }
                else
                {
                    context.Remove(old);
                    context.SaveChanges();

                }
  
            
        }

        public async Task<bool> IsValidSubCategory(int id)
        {
        
             var isvalid=await context.SubCategories.AnyAsync(c => c.Id == id);
              return isvalid;
        }
    }
}
