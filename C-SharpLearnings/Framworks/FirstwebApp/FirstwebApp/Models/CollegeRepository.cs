namespace FirstwebApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student>Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id = 1,
                Name = "naresh",
                Email = "Test",
                Address = "zxc"
            },
            new Student
            {
                Id = 2,
                Name = "karthi",
                Email = "Test",
                Address = "zxc"
            }
        };
    }
}
