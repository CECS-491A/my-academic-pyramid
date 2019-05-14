export default {
    state: {
        postQuestionForm: true,
        postAnswerForm: false,
        editQuestionForm: false,
        viewAnswers: false,
        viewPostedQuestions: true,
        school: "",
        department: "",
        course: "",
        question: null,

    },
    openPostQuestionForm() {
        this.state.postQuestionForm = true
    },
    closePostQuestionForm() {
        this.state.postQuestionForm = false
    },

    openPostAnswerForm() {
        this.state.postAnswerForm = true
    },
    closePostAnswerForm() {
        this.state.postAnswerForm = false
    },

    openEditQuestionForm(item) {
        this.state.editQuestionForm = true
        this.state.question = item
    },
    closeEditQuestionForm() {
        this.state.editQuestionForm = false
    },

    viewPostedQuestions() {
        this.state.viewPostedQuestions = true
        this.state.viewAnswers = false
    },
    viewDraftQuestions() {
        this.state.viewPostedQuestions = false
        this.state.viewAnswers = false
    },
    viewAnswers(item) {
        this.state.viewPostedQuestions = false
        this.state.viewAnswers = true
        this.state.question = item
    },

    setSchool(schoolName) {
        this.state.school = schoolName
    },
    setDepartment(departmentName) {
        this.state.department = departmentName
    },
    setCourse(courseName) {
        this.state.course = courseName
    }
}