﻿namespace Com.ChatR.Web.Controllers
{
    using Core;
    using Core.Dtos;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    public class ChatApiController : ApiController
    {
        private readonly ChatManager _chatManager;
        private readonly ChatRHub _chatRHub;
        public ChatApiController(ChatManager chatManager, ChatRHub chatRHub)
        {
            _chatManager = chatManager;
            _chatRHub = chatRHub;
        }

        [HttpPost]
        [Route("api/createroom")]
        public async Task<IHttpActionResult> CreateRoom(ChatRoomDto room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _chatManager.CreateChatRoom(room));
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/rooms/all")]
        public async Task<IHttpActionResult> GetRooms()
        {
            try
            {
                return Ok(await _chatManager.GetAllChatRooms());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/rooms/{0}/{1}")]
        public async Task<IHttpActionResult> GetRoomsPaged(int pageNo, int pageSize)
        {
            try
            {
                if (pageSize <= 0 || pageNo <= 0) return BadRequest("Invalid parameters");
                return Ok(await _chatManager.GetChatRooms(pageNo, pageSize));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPost]
        [Route("api/participant/create")]
        public async Task<IHttpActionResult> GetNewParticipant(ChatParticipantDto participant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _chatManager.CreateParticipant(participant));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        //[HttpGet]
        //[Route("api/room/join/{0}")]
        //public async Task<IHttpActionResult> JoinChatRoom(ChatParticipantDto participant)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            return Ok(await _chatManager.JoinChatRoom(participant));
        //        }
        //        else
        //            return BadRequest("Invalid parameters");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
    }
}