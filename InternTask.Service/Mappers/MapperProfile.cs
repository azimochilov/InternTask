using AutoMapper;
using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.DTOs.Users;

namespace InternTask.Service.Mappers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserCreationDto, UserResultDto>().ReverseMap();

        CreateMap<Grades, GradesCreationDto>().ReverseMap();
        CreateMap<GradesCreationDto, GradesResultDto>().ReverseMap();
        CreateMap<Grades, GradesResultDto>().ReverseMap();
        CreateMap<Grades, GradesUpdateDto>().ReverseMap();

        CreateMap<Subject, SubjectCreationDto>().ReverseMap();
        CreateMap<Subject, SubjectResultDto>().ReverseMap();
        CreateMap<SubjectCreationDto, SubjectResultDto>().ReverseMap();
        CreateMap<Subject, SubjectUpdateDto>().ReverseMap();

        CreateMap<UserSubject, UserSubjectCreationDto>().ReverseMap();
        CreateMap<UserSubject, UserSubjectResultDto>().ReverseMap();
        CreateMap<UserSubjectCreationDto, UserSubjectResultDto>().ReverseMap();
        CreateMap<UserSubject, UserSubjectUpdateDto>().ReverseMap();
    }
}
