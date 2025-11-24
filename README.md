# PolicyNotesService

- Add, update, and fetch policy notes
- Inmemory database for development(RealRuntimePolicyInMemoryDb) and testing(PolicyNotesTestDb) and seeded two policynotes for working on.

- Minimal APIs mapping – Handles API requests
- Model/ – Contains the data models
- Data/ – EF Core Handles Database
- Repository/ – Handles data storage and retrieval logic 
- Services/ – Contains business logic
- PolicyNotesService.Test – Unit and Integration tests for checking functionality

- GET /notes/ – Fetch all PolicyNotes
- GET /notes/{id} - Fetch PolicyNote by 'id'
- POST /notes/ – Add a new note
- DELETE /notes/ – Delete a policy note
- PUT /notes/ – Update a policy note

- Unit Tests (4) checking modular functioning
- Integration Tests (4) checking flow of the request and asserting the results
