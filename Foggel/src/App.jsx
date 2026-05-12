import CardStatistics from "./components/CardStatistics"
import Menu from "./components/menu"
import RegNewBirdForm from "./components/regNewBirdForm"


function App() {
  

  return (
    <>
        <header>
          <div className="nav-container">
            <img className="header-bg" src="/images/birds-flying-bg-crop.png" alt="Leafs." />
            <figure>
              <img src="/images/foggel-logo.png" alt="Fôggel Logo." className="logo" />
            </figure>
          </div>
        </header>

      <div className="wrapper">  
        <main className="main-container">
          <aside className="menu-section border">
            <Menu />
          </aside>

          <section className="content-section">
            <section className="register-new-section">
              <RegNewBirdForm />
            </section>
            
            <section>
              <CardStatistics />
            </section>
          </section>
        </main>

        <footer>
          <div className="footer-container">
            &copy; 2026 BirdView. All rights reserved.
            <a href="https://www.textstudio.com/">Font generator</a>
          </div>
        </footer>
      </div>
    </>
  )
}

export default App
