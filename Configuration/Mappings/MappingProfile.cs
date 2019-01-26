namespace Configuration.Mappings
{
    using DomainBlogs = Domain.Blogs;
    using DataBlogs = Data.Blogs;
    using DomainPosts = Domain.Posts;
    using DataPosts = Data.Posts;
    using DomainComments = Domain.Comments;
    using DataComments = Data.Comments;
    using SharedDTOs = Shared.DTOs;
    using AutoMapper;

    internal class MappingProfile : Profile
    {
        internal MappingProfile()
        {
            CreateMap<DataComments.Comment, DomainComments.Comment>().ReverseMap();
            CreateMap<SharedDTOs.Comment, DomainComments.Comment>().ReverseMap();

            CreateMap<DataPosts.Post, DomainPosts.Post>().ReverseMap();
            CreateMap<SharedDTOs.Post, DomainPosts.Post>().ReverseMap();

            CreateMap<DataBlogs.Blog, DomainBlogs.Blog>().ReverseMap();
            CreateMap<SharedDTOs.Blog, DomainBlogs.Blog>().ReverseMap();
        }
    }
}