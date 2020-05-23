export class Employee {
  id: number;
  emp_name: string;
}


export class EmployeeTask {
  id: number;
  emp_id: number;
  title: string;
  description: string;
  prioritylevel: string;
  state: string;
  estimate: string;
  prioritylevel_id: number;
  state_id: number;
}

export class PriorityOrState {
  prioritylevels = new Array<Prioritylevel>();
  states = new Array<State>();
}

export class Prioritylevel {
  id: number;
  priority_level: string;
}

export class State {
  id: number;
  state_name: string;
}