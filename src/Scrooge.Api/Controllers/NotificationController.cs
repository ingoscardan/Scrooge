using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Scrooge.Api.DTOs;
using Scrooge.DbServices;
using Scrooge.DbServices.Entities;
using Scrooge.DbServices.Repositories;
using Scrooge.Services.Models;
using Scrooge.Services.Services;

namespace Scrooge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IDbRepository<NotificationEntity> _notificationDbRepository;
        private readonly IMapper _mapper;
        private readonly IDbUnitOfWork<NotificationEntity> _dbUnitOfWork;
        private readonly INotificationService _service;

        public NotificationController(
            IDbRepository<NotificationEntity> notificationDbRepository, 
            IDbUnitOfWork<NotificationEntity> dbUnitOfWork,
            INotificationService service,
            IMapper mapper)
        {
            _dbUnitOfWork = dbUnitOfWork;
            _notificationDbRepository = notificationDbRepository;
            _mapper = mapper;
            _service = service;
        }

        #region Get methods
        [HttpGet]
        public async Task<List<NotificationDtoResponse>> Get()
        {
            var result = await _service.Get();
            return _mapper.Map<List<NotificationDtoResponse>>(result);
        }
        [HttpGet("unread")]
        public async Task<List<NotificationDtoResponse>> GetUnread()
        {
            var result = await _service.GetUnreadNotifications();
            return _mapper.Map<List<NotificationDtoResponse>>(result);
        }
        [HttpGet("read")]
        public async Task<List<NotificationDtoResponse>> GetRead()
        {
            var result = await _service.GetReadNotifications();
            return _mapper.Map<List<NotificationDtoResponse>>(result);
        }
        [HttpGet("top10")]
        public async Task<List<NotificationDtoResponse>> GetTopTen()
        {
            var result = await _dbUnitOfWork.NotificationRepository.GetTop();
            return _mapper.Map<List<NotificationDtoResponse>>(result);
        }
        [HttpGet("{notificationId:guid}")]
        public async Task<IActionResult> Get(Guid notificationId)
        {
            var result = await _dbUnitOfWork.Repository.GetByIdAsync(notificationId);
            if (result is null) return await Task.FromResult<IActionResult>(NotFound());
            var response = _mapper.Map<NotificationDtoResponse>(result);
            
            return await Task.FromResult<IActionResult>(Ok(response));
       
        }
        #endregion

        #region Create Methods
        
        #endregion

        #region Action Methods

        [HttpPatch("{notificationId:guid}/markAsRead")]
        public NotificationEntity MarkAsRead(Guid notificationId)
        {
            return new NotificationEntity()
            {
                Message = "Test action",
                Status = NotificationStatus.Read,
                NotificationId = notificationId
            };
        }
        
        [HttpPatch("{notificationId:guid}/markAsUnRead")]
        public NotificationEntity MarkAsUnRead(Guid notificationId)
        {
            return new NotificationEntity()
            {
                Message = "Test action",
                Status = NotificationStatus.Unread,
                NotificationId = notificationId
            };
        }
        #endregion

        #region Delete Methods
        [HttpDelete("{notificationId:guid}")]
        public NotificationEntity Delete(Guid notificationId)
        {
            return new NotificationEntity()
            {
                Message = "Test action",
                Status = NotificationStatus.Deleted,
                NotificationId = notificationId
            };
        }
        #endregion
        
    }
}
