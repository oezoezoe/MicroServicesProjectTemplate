using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Framework.Core.Cqrs;

namespace MicroServiceTemplate.Api
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ICommandDispatcher _commandDispatcher;
        protected readonly IMapper _mapper;
        protected readonly IQueryDispatcher _queryDispatcher;

        protected BaseApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IMapper mapper)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher), $"{nameof(commandDispatcher)} is null.");
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher), $"{nameof(queryDispatcher)} is null.");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), $"{nameof(mapper)} is null.");
        }
    }
}