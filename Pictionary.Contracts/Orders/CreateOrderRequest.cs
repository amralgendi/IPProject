namespace Pictionary.Contracts.Orders;

public record CreateOrderRequest();

// firstName: address.firstName,
//         lastName: address.lastName,
//         address: address.address,
//         city: address.city,
//         phoneNumber: address.phoneNumber,
//         floorNumber: address.floorNumber,
//         apartmentNumber: address.apartmentNumber,
//       }

public record CreateAddressRequest(
    string FirstName,
    string LastName,
    string Line1,
    string? Line2,
    string City,
    string State,
    string? Floor,
    string? Apt);