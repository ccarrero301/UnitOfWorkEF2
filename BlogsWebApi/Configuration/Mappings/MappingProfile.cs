namespace BlogsWebApi.Configuration.Mappings
{
    using DomainBlogs = Domain.Blogs;
    using DataBlogs = Data.Blogs;
    using DomainPosts = Domain.Posts;
    using DataPosts = Data.Posts;
    using DomainComments = Domain.Comments;
    using DataComments = Data.Comments;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataComments.Comment, DomainComments.Comment>();
            CreateMap<DomainComments.Comment, DataComments.Comment>();

            CreateMap<DataPosts.Post, DomainPosts.Post>();
            CreateMap<DomainPosts.Post, DataPosts.Post>();

            CreateMap<DataBlogs.Blog, DomainBlogs.Blog>();
            CreateMap<DomainBlogs.Blog, DataBlogs.Blog>();
        }
    }
}