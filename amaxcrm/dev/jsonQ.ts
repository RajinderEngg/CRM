class jsonQ{
    private jqlsArr:Object;
    private jqlsObject:any;

    constructor(private _TransationName:string,private _SHash:string){
        this.jqlsArr = null;
        this.jqlsObject=null;
        this.jqlsObject={
            TransationName: _TransationName,
            SHash: _SHash,
            Jqls: []
        }
    }

    public addToList(){
        if(this.jqlsArr!=null){
            this.jqlsObject.Jqls.push(this.jqlsArr);
            this.jqlsArr=null;
            return true;
        }else return false;
    }
    public toJsonQObject():any{
        this.addToList();
        return this.jqlsObject;
    }
    private throwIfInvalidOperation(Operation:number){
        if(this.jqlsArr==null || this.jqlsArr["Type"]!=Operation) throw 'Invalid operation';
    }


    //For Insert Statement
    public addNewInsert(TableName:string,PrimaryField:string,PKeyName:string=""):boolean{
        if(this.jqlsArr==null){
            this.jqlsArr = {
                Type: 1,
                Table: TableName,
                PrimaryField: PrimaryField,
                PKeyName: PKeyName,
                Condictions: { },
                Fields: { }
            }
            return true;
        }else return false;
    }
    public Insert(FieldName:string,Value:string){
        this.throwIfInvalidOperation(1);
        this.jqlsArr["Fields"][FieldName]={
            Value: Value,
            IsFkey: false
        }
    }
    public InsertForignKey(FieldName:string,Value:string){
        this.throwIfInvalidOperation(1);
        this.jqlsArr["Fields"][FieldName]={
            Value: Value,
            IsFkey: true
        }
    }

    //For Update Statement
    public addNewUpdate(TableName:string){
        throw 'Not Implemented';
    }
    public Update(FieldName:string,Value:string){
        throw 'Not implemented';
    }


    //For select data fro server
    public addNewSelect(TableName:string){
        if(this.jqlsArr==null){
            this.jqlsArr = {
                Type: 6,
                Table: TableName,
                PrimaryField: "",
                PKeyName: "",
                Condictions: new Object(),
                Fields: new Object()
            }
            return true;
        }else return false;
    }
    public select(FieldName:string,AsAlies:string){
        this.throwIfInvalidOperation(6);
        this.jqlsArr["Fields"][FieldName]={
            Value: AsAlies||FieldName
        }
    }
    public Condiction(FieldName:string,AsAlies:string){
        this.throwIfInvalidOperation(6);
        this.jqlsArr["Fields"][FieldName]={
            Value: AsAlies||FieldName
        }
    }
}

export {jsonQ}