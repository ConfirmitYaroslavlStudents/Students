using System.Web.Mvc;
using ToDoLibrary.Commands;
using TodoWeb.Requests;

namespace TodoWeb
{
    public class WebMediator : IMediator
    {
        private readonly RequestHandler _requestHandler;
        private readonly CommandCreator _commandCreator = new CommandCreator();

        public WebMediator(RequestHandler requestHandler)
            => _requestHandler = requestHandler;

        public void Send(Request request)
        {
            ICommand command;
            switch (request)
            {
                case GetRequest getRequest:
                    getRequest.Tasks = _requestHandler.ShowTasks();
                    return;
                case PostRequest postRequest:
                    command = _commandCreator.CreateCommandForPostRequest(postRequest);
                    postRequest.HandlingResult = _requestHandler.Handle(HttpVerbs.Post, command);
                    return;
                case DeleteRequest deleteRequest:
                    command = _commandCreator.CreateCommandForDeleteRequest(deleteRequest);
                    deleteRequest.HandlingResult = _requestHandler.Handle(HttpVerbs.Delete, command);
                    return;
                case PatchRequest patchRequest:
                    command = _commandCreator.CreateCommandForPatchRequest(patchRequest);
                    patchRequest.HandlingResult = _requestHandler.Handle(HttpVerbs.Patch, command);
                    return;
            }
        }
    }
}