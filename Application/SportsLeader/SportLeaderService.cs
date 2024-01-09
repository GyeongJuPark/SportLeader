using AutoMapper;
using SportLeader.Controllers.Client.Request;
using SportLeader.DTO;
using SportLeader.Models;
using SportLeader.Repository;

namespace SportLeader.Application.SportsLeader
{
    public interface ISportLeaderService
    {
        void Create(RegisterSportsLeaderRequest LeaderWorkInfo);
        LeaderInfoDto Update(string leaderId);
        void Update(UpdateSportsLeaderRequest model);
        LeaderInfoDto Read(string leaderId);
        void Delete(string[] leaderNos);
        IEnumerable<LeaderInfoDto> GetAllList();
        IEnumerable<LeaderInfoDto> GetAllList(string LeaderNo);
        IEnumerable<LeaderDto> GetLeaderList();
        IEnumerable<SchoolDto> GetSchoolList();
        IEnumerable<SportDto> GetSportList();
    }

    public class SportLeaderService : ISportLeaderService
    {
        private readonly LeaderRepository _leaderRepository;
        private readonly IMapper _mapper;
        public SportLeaderService(LeaderRepository leaderRepository, IMapper mapper)
        {
            _leaderRepository = leaderRepository;
            _mapper = mapper;
        }

        // 등록
        public void Create(RegisterSportsLeaderRequest LeaderWorkInfo)
        {
            _leaderRepository.Create(LeaderWorkInfo);
        }

        // 수정
        public LeaderInfoDto Update(string leaderId)
        {
            var leader = _leaderRepository.GetAllList(leaderId)
                .FirstOrDefault(r => r.LeaderNo == leaderId);

            var leaderDTO = _mapper.Map<LeaderInfoDto>(leader);

            return leaderDTO;
        }
        public void Update(UpdateSportsLeaderRequest model)
        {
            _leaderRepository.Modify(model);
        }
        // 조회
        public LeaderInfoDto Read(string leaderId)
        {
            var leader = _leaderRepository.GetAllList(leaderId)
                .FirstOrDefault(r => r.LeaderNo == leaderId);

            var leaderDTO = _mapper.Map<LeaderInfoDto>(leader);

            return leaderDTO;
        }

        // 삭제 
        public void Delete(string[] leaderNos)
        {
            _leaderRepository.Delete(leaderNos);
        }

        public IEnumerable<SportDto> GetSportList()
        {
            var result = _leaderRepository.GetSportList();
            var sportsDto = _mapper.Map<IEnumerable<SportDto>>(result);
            return sportsDto;
        }

        public IEnumerable<LeaderDto> GetLeaderList()
        {
            var result = _leaderRepository.GetLeaderList();
            var leadersDto = _mapper.Map<IEnumerable<LeaderDto>>(result);
            return leadersDto;
        }

        public IEnumerable<LeaderInfoDto> GetAllList()
        {
            var result = _leaderRepository.GetAllList();
            var leaderListDto = result.Select(leader => new LeaderInfoDto
            {
                LeaderNo = leader.LeaderNo,
                LeaderName = leader.LeaderName,
                SchoolNo = leader.T_School.SchoolName,
                SportsNo = leader.T_Sport.SportsName,
            });
            return leaderListDto;
        }

        public IEnumerable<LeaderInfoDto> GetAllList(string LeaderNo)
        {
            var result = _leaderRepository.GetAllList(LeaderNo);
            var leaderInfoDto = _mapper.Map<IEnumerable<LeaderInfoDto>>(result);
            return leaderInfoDto;
        }

        public IEnumerable<SchoolDto> GetSchoolList()
        {
            var schools = _leaderRepository.GetSchoolList();
            var schoolDTO = _mapper.Map<IEnumerable<SchoolDto>>(schools);

            return schoolDTO;
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<T_LeaderWorkInfo, LeaderInfoDto>()
                    .ForMember(dest => dest.Histories, opt => opt.MapFrom(src => src.T_History.Select(history => new HistoryDto
                    {
                        LeaderNo = src.LeaderNo,
                        SchoolName = history.SchoolName,
                        StartDT = history.StartDT,
                        EndDT = history.EndDT,
                        SportsNo = history.T_Sport.SportsName
                    })))
                    .ForMember(dest => dest.Certificates, opt => opt.MapFrom(src => src.T_Certificate.Select(certificate => new CertificateDto
                    {
                        LeaderNo = src.LeaderNo,
                        CertificateName = certificate.CertificateName,
                        CertificateNo = certificate.CertificateNo,
                        CertificateDT = certificate.CertificateDT,
                        Organization = certificate.Organization
                    })))
                    .ForMember(dest => dest.LeaderImage, opt => opt.MapFrom(src => src.T_LeaderImage.LeaderImage))
                    .ForMember(dest => dest.SportsNo, opt => opt.MapFrom(src => src.T_Sport.SportsName))
                    .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.T_School.SchoolName));

                CreateMap<T_Leader, LeaderDto>()
                    .ForMember(dest => dest.LeaderNo, opt => opt.MapFrom(src => src.LeaderNo))
                    .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.LeaderName));

                CreateMap<T_Sport, SportDto>()
                    .ForMember(dest => dest.SportsNo, opt => opt.MapFrom(src => src.SportsNo))
                    .ForMember(dest => dest.SportsName, opt => opt.MapFrom(src => src.SportsName));

                CreateMap<T_School, SchoolDto>()
                    .ForMember(dest => dest.SchoolNo, opt => opt.MapFrom(src => src.SchoolNo))
                    .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.SchoolName));

                CreateMap<T_History, HistoryDto>();
                CreateMap<T_Certificate, CertificateDto>();
                CreateMap<T_LeaderImage, LeaderImageDto>();
            }
        }

    }
}

