﻿using Application.DTO.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Genres
{
    public interface IUpdateGenreCommand : ICommand<GenreDto>
    {
    }
}
