# DotNet API - Items and Auctions

This API provides the backend functionality for managing **Items** and **Auctions** in a marketplace. It handles auction creation, auction status updates, and item management. This API will later interact with a **Spring-based API** responsible for the **Bidding** business logic.

## Features

- **Auction Creation**: Allows the creation of auctions for items, ensuring validation for start and end dates.
- **Auction Status Management**: Periodically updates auction statuses based on their start and end dates (open, closed, sold, pending).
- **Item Management**: Ensures items cannot be associated with multiple active auctions and automatically updates item statuses based on auction outcomes.
- **Background Service**: Handles the automated auction status updates on a scheduled basis.

## EndPoints
## Postman File
- **Inside the project folder theres a file with all my postman endpoints**

## Future Updates:

- **More Stats**: We plan to add more statistics regarding auctions and items (e.g., bid history, total bids, bid progress).
- **Change Automatic Updates**: Enhancements to allow dynamic intervals for the background service updates (rather than a fixed 24-hour interval).
- **Real-time Bidding**: Integration with the **Spring-based API** for real-time bidding functionalities, allowing live updates on auction bids.
- **User Authentication & Authorization**: Implement authentication to secure access to endpoints, allowing role-based access for auction managers, users, and admins.



## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

