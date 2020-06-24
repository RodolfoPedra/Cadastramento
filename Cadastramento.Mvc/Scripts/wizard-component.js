export default function descriptionAnimal() {
    const tabMenu = document.querySelectorAll('.js-tab li');
    const boxWizard = document.querySelectorAll('.box-wizard');
    const tabConteudo = document.querySelectorAll('[data-anime]');
    tabConteudo[0].classList.add('ativo');

    function actionTab(index){
        tabConteudo.forEach(section => {
            section.classList.remove('ativo');
        })

        for(let i = 0; i < index; i++) {
            tabMenu[i].classList.add('il-unselected');
            boxWizard[i].classList.add('bw-unselected');
        }

        tabConteudo[index].classList.add('ativo');
        tabMenu[index].classList.remove('il-unselected');
        boxWizard[index].classList.remove('bw-unselected');
        tabMenu[index].classList.add('il-selected');
        boxWizard[index].classList.add('bw-selected');
    }

    function removeTab(index) {

        for(let i = index + 1; i < tabMenu.length; i++) {
                tabMenu[i].classList.remove('il-selected');
                boxWizard[i].classList.remove('bw-selected');
                tabMenu[i].classList.remove('il-unselected');
                boxWizard[i].classList.remove('bw-unselected');
        }
    }
    
    tabMenu.forEach((li, index) => {
        li.addEventListener('click', () => {
            actionTab(index);
            removeTab(index);   
        })
    });
}

descriptionAnimal();