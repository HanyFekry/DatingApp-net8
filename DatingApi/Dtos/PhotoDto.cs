﻿namespace DatingApi.Dtos
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
    }
}