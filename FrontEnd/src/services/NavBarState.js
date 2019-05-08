export default {
    state: {
        newMessage: false,
        drawerIsOpen: false
    },
    swapDrawer() {
        this.state.drawerIsOpen = !this.state.drawerIsOpen
    },
    aNewMessageExists() {
        this.state.newMessage = true;
    },
    messageIsRead() {
        this.state.newMessage = false;
    }
}