﻿using Api.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator = null!;

    private IMapper _mapper = null!;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected IMapper Mapper =>
        _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected ActionResult Success<T>(T response, object? meta = null)
    {
        var result = new SuccessResponse<T>();

        if (response is IList)
        {
            result.Items = response;
        }
        else
        {
            result.Item = response;
        }

        if (meta != null)
        {
            result.Meta = meta;
        }

        return Ok(result);
    }

    protected ActionResult Success(object? meta = null)
    {
        var result = new SuccessResponse<object>();

        if (meta != null)
        {
            result.Meta = meta;
        }

        return Ok(result);
    }
}
