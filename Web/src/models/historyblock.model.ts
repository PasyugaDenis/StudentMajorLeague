export class HistoryBlock {
	public Id: number;
	public CreatedOn: Date;
	public Changes: string;
	public Hash: string;
	public PreviousHash: string;
	public AuthorId: number;
	public ChainId: number;

	constructor() {
        this.Id = 0;
        this.CreatedOn = new Date();
		this.Changes = "";
		this.Hash = "";
		this.PreviousHash = "";
		this.AuthorId = 0;
		this.ChainId = 0;
	}
};