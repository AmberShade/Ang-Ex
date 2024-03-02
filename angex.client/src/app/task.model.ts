export class Task {
  id=-1;
  name: string="sample";
  created: Date=new Date();
  deadl?: Date;
  descr?: string;
  tags?: string;
  priority: number=-1;
  parent: number=-1;
  state: number = -1;
  public clone() {
    var copy = new Task();
    copy.id = this.id;
    copy.name = this.name;
    copy.deadl = this.deadl;
    copy.descr = this.descr;
    copy.tags = this.tags;
    copy.priority = this.priority;
    copy.parent = this.parent;
    copy.state = this.state;
    return copy;
  }
}
