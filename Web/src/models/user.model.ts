export class UserRegistrationRequestModel {
	public Name: string;
	public Surname: string;
	public Email: string;
	public Password: string;
	public Birthday: Date;
	public LeagueId: number;

	constructor() {
		this.Name = "";
		this.Surname = "";
		this.Email = "";
		this.Password = "";
		this.Birthday = new Date();
		this.LeagueId = 0;
	}
};

export class UserAuthorizationRequestModel {
	public Email: string;
	public Password: string;
};

export class UserResponseModel {
	public Id: number;
	public Email: string;
	public RegistrationDate: Date;
	public RoleId: number;
	public Name: string;
	public Surname: string;
	public Birthday: Date;
	public Education: string;
	public Phone: string;
	public City: string;
	public LeagueId: number;
	public Height: number;
	public Weight: number;

	constructor() {
		this.Id = 0;
		this.Email = "";
		this.RegistrationDate = new Date();
		this.RoleId = 0;
		this.Name = "";
		this.Surname = "";
		this.Birthday = new Date();
		this.Education = "";
		this.Phone = "";
		this.City = "";
		this.LeagueId = 0;
		this.Height = null;
		this.Weight = null;
	}
};