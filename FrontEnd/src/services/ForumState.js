export default {
    state: {
        schoolQuestions: true,
        departmentQuestions: false,
        courseQuestions: false,
        draftQuestions: false,
        answers: false
    },
    schoolQuestions() {
        this.state.schoolQuestions = true;
        this.state.departmentQuestions = false;
        this.state.courseQuestions = false;
        this.state.draftQuestions = false; 
        this.state.answers = false;
    },
    departmentQuestions() {
        this.state.schoolQuestions = false;
        this.state.departmentQuestions = true;
        this.state.courseQuestions = false;
        this.state.draftQuestions = false; 
        this.state.answers = false;    },
    courseQuestions() {
        this.state.schoolQuestions = false;
        this.state.departmentQuestions = false;
        this.state.courseQuestions = true;
        this.state.draftQuestions = false; 
        this.state.answers = false;    },
    draftQuestions() {
        this.state.schoolQuestions = false
        this.state.departmentQuestions = false;
        this.state.courseQuestions = false;
        this.state.draftQuestions = true; 
        this.state.answers = false;    },
    answers() {
        this.state.schoolQuestions = false;
        this.state.departmentQuestions = false;
        this.state.courseQuestions = false;
        this.state.draftQuestions = false; 
        this.state.answers = true;    },

}