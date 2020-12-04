using APIstart.Models;
using APIstart.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstart.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, o => o.MapFrom(x => x.Category.Name));


            CreateMap<Book, BookDto>()
                            .ForMember(x => x.Genre, o => o.MapFrom(x => x.GenreBooks.Select(b => b.Genre.Name)));


            





            







            CreateMap<Student, StudentDto>()
                .ForMember(x => x.StudentCourses, o => o.MapFrom(x => x.StudentCourses.Select(b => new
                {
                    Course = b.Course.Name,
                    CourseId = b.CourseId
                })));

        }
    }
}
