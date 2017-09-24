using BuilderWebApp3.Helper_Classes.DAOInterfaces;
using BuilderWebApp3.Models;

namespace BuilderWebApp3.Helper_Classes.DAOImplementations {
    public class ModelFactory {

        public ModelDAOInterface GetAppropriateModel(object model)
        {
            if (model is Session)
                return new SessionDAOImpl();
            if (model is Builder)
                return new BuilderDAOImpl();
            if (model is Project)
                return new ProjectDAOImpl();
            if(model is BuilderProjectLink)
                return new BuilderProjectLinkDAOImpl();

            return null;
        }
    }
}