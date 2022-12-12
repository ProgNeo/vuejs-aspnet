﻿using Anime.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_project.Controllers;

public class BaseController : ControllerBase
{
    protected readonly AnimeContext _context;

    protected enum Roles
    {
        User,
        Moderator,
        Superuser
    }
    
    public BaseController(AnimeContext context)
    {
        _context = context;
    }

    protected long? GetSession()
    {
        if (HttpContext.Request.Cookies["session_id"] == null) return null;
        
        try
        {
            return long.Parse(HttpContext.Request.Cookies["session_id"]!);
        }
        catch (FormatException)
        {
            return null;
        }
    }

    protected bool CheckSession()
    {
        if (HttpContext.Request.Cookies["session_id"] == null) return false;
        
        try
        {
            var sessionId = long.Parse(HttpContext.Request.Cookies["session_id"]!);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected async Task<IEnumerable<string>> GetRolesByUser()
    {
        var sessionId = GetSession();

        if (sessionId == null)
        {
            return new List<string>();
        }
        
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
        var user = await _context.Users
            .Include(u => u.UsersRoles)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(u => u.Id == session!.UserId);

        return user!.UsersRoles.Select(e => e.Role.Name);
    }
}