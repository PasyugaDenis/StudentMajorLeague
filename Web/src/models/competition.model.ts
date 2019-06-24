export class Competition {
	public Id: number;
	public Title: string;
	public Type: string;
	public Date: Date;
	public Location: string;
	public Description: string;

	constructor() {
		this.Id = 0;
		this.Title = "";
		this.Type = "";
		this.Date = new Date();
		this.Location = "";
		this.Description = "";
	}
};