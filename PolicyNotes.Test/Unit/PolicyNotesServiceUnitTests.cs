using Moq;
using PolicyNotesService.Model;
using PolicyNotesService.Repository;
using PolicyNotesService.Sevices;

namespace PolicyNotes.Test.Unit
{
    public class PolicyNotesServiceUnitTests
    {
        [Fact]
        public async Task AddPolicyNote_Should_Add_And_Return()
        {
            var mockRepo = new Mock<IPolicyRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Policy>()))
                    .ReturnsAsync((Policy p) => { p.Id = 1; return p; });

            var service = new PolicyService(mockRepo.Object);

            var result = await service.AddPolicyNoteAsync("PN", "Note");

            Assert.NotNull(result);
            Assert.Equal("PN", result.PolicyNumber);
            Assert.Equal("Note", result.Note);
        }

        [Fact]
        public async Task GetAllNotes_Should_Return_List()
        {
            var mockRepo = new Mock<IPolicyRepository>();
            mockRepo.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(new List<Policy>
                    {
                        new Policy{ Id = 1, PolicyNumber="P1", Note="A"},
                        new Policy{ Id = 2, PolicyNumber="P2", Note="B"}
                    });

            var service = new PolicyService(mockRepo.Object);

            var result = await service.GetAllNotesAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetNoteById_Should_Return_Policy_When_Found()
        {
            var mockRepo = new Mock<IPolicyRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(new Policy
                    {
                        Id = 1,
                        PolicyNumber = "P100",
                        Note = "Test Note"
                    });

            var service = new PolicyService(mockRepo.Object);

            var result = await service.GetNoteByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("P100", result.PolicyNumber);
            Assert.Equal("Test Note", result.Note);
        }

        [Fact]
        public async Task GetNoteById_Should_Return_Null_When_Not_Found()
        {
            var mockRepo = new Mock<IPolicyRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(999))
                    .ReturnsAsync((Policy?)null);

            var service = new PolicyService(mockRepo.Object);

            var result = await service.GetNoteByIdAsync(999);

            Assert.Null(result);
        }
    }
}