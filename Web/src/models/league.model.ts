export class League {
	public Id: number;
	public Title: string;
	public Description: string;
	public MainLeagueId: number;

	constructor() {
		this.Id = 0;
		this.Title = "";
		this.Description = "";
		this.MainLeagueId = 0;
	}
};