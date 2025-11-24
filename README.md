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
<img width="722" height="457" alt="Test Explorer" src="https://github.com/user-attachments/assets/1cfe21f8-786f-45cb-a489-6f858e99eb09" />
<img width="1204" height="590" alt="Running APIs" src="https://github.com/user-attachments/assets/fc2f6914-e5ad-4d80-a644-55f1f5d2726e" />
<img width="381" height="607" alt="Project Structure" src="https://github.com/user-attachments/assets/7199d426-d6d3-4eef-9c89-22ebbd490524" />
