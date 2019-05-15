export default {
    state: {
        postQuestionForm: false,
        postAnswerForm: false,
        editQuestionForm: false,
        viewAnswers: false,
        viewPostedQuestions: true,
        school: "",
        department: "",
        course: "",
        question: null,
        questionsToGet: "",

    },
    openPostQuestionForm() {
        this.state.postQuestionForm = true
    },
    closePostQuestionForm() {
        this.state.postQuestionForm = false
    },

    openPostAnswerForm(item) {
        this.state.postAnswerForm = true
        this.question = item
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
    },

    getSchoolQuestions() {
        this.state.questionsToGet = "school"
    },
    getDepartmentQuestions() {
        this.state.questionsToGet = "department"
    },
    getCourseQuestions() {
        this.state.questionsToGet = "course"
    },
}